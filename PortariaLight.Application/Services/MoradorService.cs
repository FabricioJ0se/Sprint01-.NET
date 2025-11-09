using PortariaLight.Domain.Entities;
using PortariaLight.Domain.Repositories;

namespace PortariaLight.Application.Services
{
    public class MoradorService : IMoradorService
    {
        private readonly IMoradorRepository _moradorRepository;

        public MoradorService(IMoradorRepository moradorRepository)
        {
            _moradorRepository = moradorRepository;
        }

        public async Task<IEnumerable<Morador>> GetAllMoradoresAsync()
        {
            return await _moradorRepository.GetAllAsync();
        }

        public async Task<Morador?> GetMoradorByIdAsync(int id)
        {
            return await _moradorRepository.GetByIdAsync(id);
        }

        public async Task<Morador> CreateMoradorAsync(Morador morador)
        {
            return await _moradorRepository.CreateAsync(morador);
        }

        public async Task<Morador> UpdateMoradorAsync(Morador morador)
        {
            return await _moradorRepository.UpdateAsync(morador);
        }

        public async Task<bool> DeleteMoradorAsync(int id)
        {
            return await _moradorRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<Morador>> GetMoradoresByApartamentoAsync(int apartamentoId)
        {
            return await _moradorRepository.GetByApartamentoIdAsync(apartamentoId);
        }

        public async Task<Morador?> GetMoradorByContatoAsync(string contato)
        {
            return await _moradorRepository.GetByContatoAsync(contato);
        }
    }
}