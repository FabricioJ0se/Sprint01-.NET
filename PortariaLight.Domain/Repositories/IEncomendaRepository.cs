using PortariaLight.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PortariaLight.Domain.Repositories
{
    public interface IEncomendaRepository
    {
        Task<IEnumerable<Encomenda>> GetAllAsync();
        Task<Encomenda> GetByIdAsync(int id);
        Task AddAsync(Encomenda encomenda);
        Task UpdateAsync(Encomenda encomenda);
        Task DeleteAsync(int id);
    }
}