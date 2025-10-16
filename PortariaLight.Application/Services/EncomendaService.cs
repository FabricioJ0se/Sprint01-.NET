using PortariaLight.Application.DTOs;
using PortariaLight.Domain.Entities;
using PortariaLight.Domain.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PortariaLight.Application.Services
{
    public class EncomendaService : IEncomendaService
    {
        private readonly IEncomendaRepository _repository;

        public EncomendaService(IEncomendaRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<EncomendaDTO>> ListarEncomendasAsync()
        {
            var encomendas = await _repository.GetAllAsync();
            return encomendas.Select(e => new EncomendaDTO
            {
                IdEncomenda = e.IdEncomenda,
                Descricao = e.Descricao,
                DataRecebida = e.DataRecebida,
                Status = e.Status,
                MoradorId = e.MoradorId,
                RetiradaId = e.RetiradaId
            });
        }

        public async Task<EncomendaDTO?> BuscarEncomendaAsync(int id)
        {
            var e = await _repository.GetByIdAsync(id);
            if (e == null) return null;

            return new EncomendaDTO
            {
                IdEncomenda = e.IdEncomenda,
                Descricao = e.Descricao,
                DataRecebida = e.DataRecebida,
                Status = e.Status,
                MoradorId = e.MoradorId,
                RetiradaId = e.RetiradaId
            };
        }

        public async Task CriarEncomendaAsync(EncomendaDTO dto)
        {
            var encomenda = new Encomenda
            {
                Descricao = dto.Descricao,
                DataRecebida = dto.DataRecebida,
                Status = dto.Status,
                MoradorId = dto.MoradorId,
                RetiradaId = dto.RetiradaId.HasValue ? dto.RetiradaId.Value : 0
            };

            await _repository.AddAsync(encomenda);
        }

        public async Task AtualizarEncomendaAsync(EncomendaDTO dto)
        {
            var encomenda = new Encomenda
            {
                IdEncomenda = dto.IdEncomenda,
                Descricao = dto.Descricao,
                DataRecebida = dto.DataRecebida,
                Status = dto.Status,
                MoradorId = dto.MoradorId,
                RetiradaId = dto.RetiradaId.HasValue ? dto.RetiradaId.Value : 0
            };

            await _repository.UpdateAsync(encomenda);
        }

        public async Task RemoverEncomendaAsync(int id) =>
            await _repository.DeleteAsync(id);
    }
}
