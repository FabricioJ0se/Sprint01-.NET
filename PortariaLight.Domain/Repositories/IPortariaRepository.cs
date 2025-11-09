using PortariaLight.Domain.Entities;

namespace PortariaLight.Domain.Repositories
{
    public interface IPortariaRepository
    {
        Task<IEnumerable<Portaria>> GetAllAsync();
        Task<Portaria?> GetByIdAsync(int id);
        Task<Portaria> CreateAsync(Portaria portaria);
        Task<Portaria> UpdateAsync(Portaria portaria);
        Task<bool> DeleteAsync(int id);
    }
}