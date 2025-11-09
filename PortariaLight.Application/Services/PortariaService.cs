using PortariaLight.Domain.Entities;
using PortariaLight.Domain.Repositories;

namespace PortariaLight.Application.Services
{
    public class PortariaService : IPortariaService
    {
        private readonly IPortariaRepository _portariaRepository;

        public PortariaService(IPortariaRepository portariaRepository)
        {
            _portariaRepository = portariaRepository;
        }

        public async Task<IEnumerable<Portaria>> GetAllPortariasAsync()
        {
            return await _portariaRepository.GetAllAsync();
        }

        public async Task<Portaria?> GetPortariaByIdAsync(int id)
        {
            return await _portariaRepository.GetByIdAsync(id);
        }

        public async Task<Portaria> CreatePortariaAsync(Portaria portaria)
        {
            return await _portariaRepository.CreateAsync(portaria);
        }

        public async Task<Portaria> UpdatePortariaAsync(Portaria portaria)
        {
            return await _portariaRepository.UpdateAsync(portaria);
        }

        public async Task<bool> DeletePortariaAsync(int id)
        {
            return await _portariaRepository.DeleteAsync(id);
        }
    }
}