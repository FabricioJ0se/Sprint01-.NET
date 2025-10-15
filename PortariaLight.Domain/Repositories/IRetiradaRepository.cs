using PortariaLight.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PortariaLight.Domain.Repositories
{
    public interface IRetiradaRepository
    {
        Task<IEnumerable<Retirada>> GetAllAsync();
        Task<Retirada> GetByIdAsync(int id);
        Task AddAsync(Retirada retirada);
        Task UpdateAsync(Retirada retirada);
        Task DeleteAsync(int id);
    }
}