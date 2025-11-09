using PortariaLight.Domain.Entities;

namespace PortariaLight.Domain.Repositories
{
    public interface IRetiradaRepository
    {
        Task<IEnumerable<Retirada>> GetAllAsync();
        Task<Retirada?> GetByIdAsync(int id);
        Task<Retirada> CreateAsync(Retirada retirada);
        Task<Retirada> UpdateAsync(Retirada retirada);
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<Retirada>> GetByEncomendaIdAsync(int encomendaId);
        Task<IEnumerable<Retirada>> GetByMoradorIdAsync(int moradorId);
    }
}