using PortariaLight.Domain.Entities;

namespace PortariaLight.Domain.Repositories;

public interface ILogAcessoRepository
{
    Task InserirAsync(LogAcesso log);
    Task<IEnumerable<LogAcesso>> GetUltimosAsync(int quantidade = 100);
    Task<IEnumerable<LogAcesso>> GetPorEndpointAsync(string endpoint);
}