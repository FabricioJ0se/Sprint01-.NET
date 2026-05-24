using Microsoft.Extensions.Options;
using MongoDB.Driver;
using PortariaLight.Domain.Entities;
using PortariaLight.Domain.Repositories;

namespace PortariaLight.Infrastructure.Repositories;

public class MongoDbSettings
{
    public string ConnectionString { get; set; } = string.Empty;
    public string DatabaseName { get; set; } = "portarialight";
}

public class LogAcessoRepository : ILogAcessoRepository
{
    private readonly IMongoCollection<LogAcesso>? _collection;

    public LogAcessoRepository(IOptions<MongoDbSettings> settings)
    {
        try
        {
            var clientSettings = MongoClientSettings.FromConnectionString(settings.Value.ConnectionString);
            clientSettings.ServerSelectionTimeout = TimeSpan.FromSeconds(2);

            var client = new MongoClient(clientSettings);
            var database = client.GetDatabase(settings.Value.DatabaseName);
            _collection = database.GetCollection<LogAcesso>("log_acessos");

            var idx = new CreateIndexModel<LogAcesso>(
                Builders<LogAcesso>.IndexKeys.Ascending(l => l.Timestamp),
                new CreateIndexOptions { ExpireAfter = TimeSpan.FromDays(30) });
            _collection.Indexes.CreateOne(idx);
        }
        catch
        {
            _collection = null;
        }
    }

    public async Task InserirAsync(LogAcesso log)
    {
        if (_collection == null) return;
        await _collection.InsertOneAsync(log);
    }

    public async Task<IEnumerable<LogAcesso>> GetUltimosAsync(int quantidade = 100)
    {
        if (_collection == null) return Enumerable.Empty<LogAcesso>();
        return await _collection.Find(_ => true)
            .SortByDescending(l => l.Timestamp).Limit(quantidade).ToListAsync();
    }

    public async Task<IEnumerable<LogAcesso>> GetPorEndpointAsync(string endpoint)
    {
        if (_collection == null) return Enumerable.Empty<LogAcesso>();
        return await _collection.Find(l => l.Endpoint.Contains(endpoint))
            .SortByDescending(l => l.Timestamp).ToListAsync();
    }
}