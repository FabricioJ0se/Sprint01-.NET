using PortariaLight.Domain.Entities;
using PortariaLight.Domain.Repositories;

namespace PortariaLight.Application.Services
{
    public class EncomendaService : IEncomendaService
    {
        private readonly IEncomendaRepository _encomendaRepository;
        private readonly IMoradorRepository _moradorRepository;
        private readonly IRetiradaRepository _retiradaRepository;

        public EncomendaService(
            IEncomendaRepository encomendaRepository,
            IMoradorRepository moradorRepository,
            IRetiradaRepository retiradaRepository)
        {
            _encomendaRepository = encomendaRepository;
            _moradorRepository = moradorRepository;
            _retiradaRepository = retiradaRepository;
        }

        public async Task<IEnumerable<Encomenda>> GetAllEncomendasAsync()
        {
            return await _encomendaRepository.GetAllAsync();
        }

        public async Task<Encomenda?> GetEncomendaByIdAsync(int id)
        {
            return await _encomendaRepository.GetByIdAsync(id);
        }

        public async Task<Encomenda> CreateEncomendaAsync(Encomenda encomenda)
        {
            // Verificar se o morador existe
            var morador = await _moradorRepository.GetByIdAsync(encomenda.IdMorador);
            if (morador == null)
                throw new ArgumentException("Morador não encontrado");

            return await _encomendaRepository.CreateAsync(encomenda);
        }

        public async Task<Encomenda> UpdateEncomendaAsync(Encomenda encomenda)
        {
            return await _encomendaRepository.UpdateAsync(encomenda);
        }

        public async Task<bool> DeleteEncomendaAsync(int id)
        {
            return await _encomendaRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<Encomenda>> GetEncomendasByMoradorAsync(int moradorId)
        {
            return await _encomendaRepository.GetByMoradorIdAsync(moradorId);
        }

        public async Task<IEnumerable<Encomenda>> GetEncomendasNaoRetiradasAsync()
        {
            return await _encomendaRepository.GetNaoRetiradasAsync();
        }
    }
}