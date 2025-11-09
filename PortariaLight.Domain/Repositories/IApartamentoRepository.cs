using PortariaLight.Domain.Entities;

namespace PortariaLight.Domain.Repositories
{
    public interface IApartamentoRepository
    {
        Task<IEnumerable<Apartamento>> GetAllAsync();
        Task<Apartamento?> GetByIdAsync(int id);
        Task<Apartamento> CreateAsync(Apartamento apartamento);
        Task<Apartamento> UpdateAsync(Apartamento apartamento);
        Task<bool> DeleteAsync(int id);
    }
}