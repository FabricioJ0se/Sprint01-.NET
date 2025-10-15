using PortariaLight.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PortariaLight.Domain.Repositories
{
    public interface IMoradorRepository
    {
        Task<IEnumerable<Morador>> GetAllAsync();
        Task<Morador> GetByIdAsync(int id);
        Task AddAsync(Morador morador);
        Task UpdateAsync(Morador morador);
        Task DeleteAsync(int id);
    }
}