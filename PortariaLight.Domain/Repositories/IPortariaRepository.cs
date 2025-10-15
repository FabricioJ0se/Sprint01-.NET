using PortariaLight.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PortariaLight.Domain.Repositories
{
    public interface IPortariaRepository
    {
        Task<IEnumerable<Portaria>> GetAllAsync();
        Task<Portaria> GetByIdAsync(int id);
        Task AddAsync(Portaria portaria);
        Task UpdateAsync(Portaria portaria);
        Task DeleteAsync(int id);
    }
}