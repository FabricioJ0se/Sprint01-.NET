using PortariaLight.Domain.Entities;

namespace PortariaLight.Application.Services
{
    public interface IPortariaService
    {
        Task<IEnumerable<Portaria>> GetAllPortariasAsync();
        Task<Portaria?> GetPortariaByIdAsync(int id);
        Task<Portaria> CreatePortariaAsync(Portaria portaria);
        Task<Portaria> UpdatePortariaAsync(Portaria portaria);
        Task<bool> DeletePortariaAsync(int id);
    }
}