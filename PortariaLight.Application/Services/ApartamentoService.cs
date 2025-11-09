using PortariaLight.Domain.Entities;
using PortariaLight.Domain.Repositories;

namespace PortariaLight.Application.Services
{
    public class ApartamentoService : IApartamentoService
    {
        private readonly IApartamentoRepository _apartamentoRepository;

        public ApartamentoService(IApartamentoRepository apartamentoRepository)
        {
            _apartamentoRepository = apartamentoRepository;
        }

        public async Task<IEnumerable<Apartamento>> GetAllApartamentosAsync()
        {
            return await _apartamentoRepository.GetAllAsync();
        }

        public async Task<Apartamento?> GetApartamentoByIdAsync(int id)
        {
            return await _apartamentoRepository.GetByIdAsync(id);
        }

        public async Task<Apartamento> CreateApartamentoAsync(Apartamento apartamento)
        {
            return await _apartamentoRepository.CreateAsync(apartamento);
        }

        public async Task<Apartamento> UpdateApartamentoAsync(Apartamento apartamento)
        {
            return await _apartamentoRepository.UpdateAsync(apartamento);
        }

        public async Task<bool> DeleteApartamentoAsync(int id)
        {
            return await _apartamentoRepository.DeleteAsync(id);
        }
    }
}