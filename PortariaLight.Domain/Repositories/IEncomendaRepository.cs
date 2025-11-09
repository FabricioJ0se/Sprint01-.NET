using PortariaLight.Domain.Entities;

namespace PortariaLight.Domain.Repositories
{
    public interface IEncomendaRepository
    {
        Task<IEnumerable<Encomenda>> GetAllAsync();
        Task<Encomenda?> GetByIdAsync(int id);
        Task<Encomenda> CreateAsync(Encomenda encomenda);
        Task<Encomenda> UpdateAsync(Encomenda encomenda);
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<Encomenda>> GetByMoradorIdAsync(int moradorId);
        Task<IEnumerable<Encomenda>> GetNaoRetiradasAsync();
    }
}