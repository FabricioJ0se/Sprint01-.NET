using PortariaLight.Application.DTOs;
using PortariaLight.Domain.Entities;
using PortariaLight.Domain.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PortariaLight.Application.Services
{
    public class MoradorService : IMoradorService
    {
        private readonly IMoradorRepository _repository;

        public MoradorService(IMoradorRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<MoradorDTO>> ListarMoradoresAsync()
        {
            var moradores = await _repository.GetAllAsync();
            return moradores.Select(m => new MoradorDTO
            {
                IdMorador = m.IdMorador,
                Nome = m.Nome,
                Apartamento = m.Apartamento,
                Bloco = m.Bloco,
                Contato = m.Contato
            });
        }

        public async Task<MoradorDTO?> BuscarMoradorAsync(int id)
        {
            var m = await _repository.GetByIdAsync(id);
            if (m == null) return null;

            return new MoradorDTO
            {
                IdMorador = m.IdMorador,
                Nome = m.Nome,
                Apartamento = m.Apartamento,
                Bloco = m.Bloco,
                Contato = m.Contato
            };
        }

        public async Task CriarMoradorAsync(MoradorDTO DTO)
        {
            var morador = new Morador
            {
                Nome = DTO.Nome,
                Apartamento = DTO.Apartamento,
                Bloco = DTO.Bloco,
                Contato = DTO.Contato
            };

            await _repository.AddAsync(morador);
        }

        public async Task AtualizarMoradorAsync(MoradorDTO DTO)
        {
            var morador = new Morador
            {
                IdMorador = DTO.IdMorador,
                Nome = DTO.Nome,
                Apartamento = DTO.Apartamento,
                Bloco = DTO.Bloco,
                Contato = DTO.Contato
            };

            await _repository.UpdateAsync(morador);
        }

        public async Task RemoverMoradorAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }
    }
}
