using PortariaLight.Application.DTOs;
using PortariaLight.Domain.Entities;
using PortariaLight.Domain.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PortariaLight.Application.Services
{
    public class RetiradaService : IRetiradaService
    {
        private readonly IRetiradaRepository _repository;

        public RetiradaService(IRetiradaRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<RetiradaDTO>> ListarRetiradasAsync()
        {
            var retiradas = await _repository.GetAllAsync();
            return retiradas.Select(r => new RetiradaDTO
            {
                IdRetirada = r.IdRetirada,
                DataRetirada = r.DataRetirada,
                TokenRetirada = r.TokenRetirada,
                MoradorId = r.MoradorId,
                PortariaId = r.PortariaId
            });
        }

        public async Task<RetiradaDTO> BuscarRetiradaAsync(int id)
        {
            var r = await _repository.GetByIdAsync(id);
            if (r == null) return null;

            return new RetiradaDTO
            {
                IdRetirada = r.IdRetirada,
                DataRetirada = r.DataRetirada,
                TokenRetirada = r.TokenRetirada,
                MoradorId = r.MoradorId,
                PortariaId = r.PortariaId
            };
        }

        public async Task CriarRetiradaAsync(RetiradaDTO dto)
        {
            var retirada = new Retirada
            {
                DataRetirada = dto.DataRetirada,
                TokenRetirada = dto.TokenRetirada,
                MoradorId = dto.MoradorId,
                PortariaId = dto.PortariaId
            };

            await _repository.AddAsync(retirada);
        }

        public async Task AtualizarRetiradaAsync(RetiradaDTO dto)
        {
            var retirada = new Retirada
            {
                IdRetirada = dto.IdRetirada,
                DataRetirada = dto.DataRetirada,
                TokenRetirada = dto.TokenRetirada,
                MoradorId = dto.MoradorId,
                PortariaId = dto.PortariaId
            };

            await _repository.UpdateAsync(retirada);
        }

        public async Task RemoverRetiradaAsync(int id) =>
            await _repository.DeleteAsync(id);
    }
}
