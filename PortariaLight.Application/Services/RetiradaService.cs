using PortariaLight.Domain.Entities;
using PortariaLight.Domain.Repositories;

namespace PortariaLight.Application.Services
{
    public class RetiradaService : IRetiradaService
    {
        private readonly IRetiradaRepository _retiradaRepository;
        private readonly IMoradorRepository _moradorRepository;
        private readonly IEncomendaRepository _encomendaRepository;

        public RetiradaService(
            IRetiradaRepository retiradaRepository,
            IMoradorRepository moradorRepository,
            IEncomendaRepository encomendaRepository)
        {
            _retiradaRepository = retiradaRepository;
            _moradorRepository = moradorRepository;
            _encomendaRepository = encomendaRepository;
        }

        public async Task<IEnumerable<Retirada>> GetAllRetiradasAsync()
        {
            return await _retiradaRepository.GetAllAsync();
        }

        public async Task<Retirada?> GetRetiradaByIdAsync(int id)
        {
            return await _retiradaRepository.GetByIdAsync(id);
        }

        public async Task<Retirada> CreateRetiradaAsync(Retirada retirada)
        {
            // Verificar se morador existe
            var morador = await _moradorRepository.GetByIdAsync(retirada.IdMorador);
            if (morador == null)
                throw new ArgumentException("Morador não encontrado");

            // REMOVER esta verificação - Encomenda não é mais obrigatória na criação da Retirada
            // var encomenda = await _encomendaRepository.GetByIdAsync(retirada.IdEncomenda);
            // if (encomenda == null)
            //     throw new ArgumentException("Encomenda não encontrada");

            return await _retiradaRepository.CreateAsync(retirada);
        }

        public async Task<Retirada> UpdateRetiradaAsync(Retirada retirada)
        {
            return await _retiradaRepository.UpdateAsync(retirada);
        }

        public async Task<bool> DeleteRetiradaAsync(int id)
        {
            return await _retiradaRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<Retirada>> GetRetiradasByEncomendaAsync(int encomendaId)
        {
            return await _retiradaRepository.GetByEncomendaIdAsync(encomendaId);
        }

        public async Task<IEnumerable<Retirada>> GetRetiradasByMoradorAsync(int moradorId)
        {
            return await _retiradaRepository.GetByMoradorIdAsync(moradorId);
        }
    }
}