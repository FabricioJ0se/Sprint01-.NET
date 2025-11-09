using PortariaLight.Domain.Entities;

namespace PortariaLight.Application.Services
{
    public interface IEncomendaService
    {
        Task<IEnumerable<Encomenda>> GetAllEncomendasAsync();
        Task<Encomenda?> GetEncomendaByIdAsync(int id);
        Task<Encomenda> CreateEncomendaAsync(Encomenda encomenda);
        Task<Encomenda> UpdateEncomendaAsync(Encomenda encomenda);
        Task<bool> DeleteEncomendaAsync(int id);
        Task<IEnumerable<Encomenda>> GetEncomendasByMoradorAsync(int moradorId);
        Task<IEnumerable<Encomenda>> GetEncomendasNaoRetiradasAsync();
    }
}