using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace PortariaLight.Domain.Entities;

public class LogAcesso
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    public string Endpoint { get; set; } = string.Empty;
    public string Metodo { get; set; } = string.Empty;
    public int StatusCode { get; set; }
    public string UsuarioNome { get; set; } = "anonimo";
    public string CorrelationId { get; set; } = string.Empty;
    public long DuracaoMs { get; set; }
    public string? IpOrigem { get; set; }
}