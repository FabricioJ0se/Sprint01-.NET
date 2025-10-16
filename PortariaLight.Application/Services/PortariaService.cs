using PortariaLight.Application.DTOs;
using PortariaLight.Domain.Entities;
using PortariaLight.Domain.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PortariaLight.Application.Services
{
    public class PortariaService : IPortariaService
    {
        private readonly IPortariaRepository _repository;

        public PortariaService(IPortariaRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<PortariaDTO>> ListarPortariasAsync()
        {
            var portarias = await _repository.GetAllAsync();
            return portarias.Select(p => new PortariaDTO
            {
                IdPortaria = p.IdPortaria,
                NomePorteiro = p.NomePorteiro,
                Turno = p.Turno,
                Contato = p.Contato,
                DataRegistro = p.DataRegistro
            });
        }

        public async Task<PortariaDTO> BuscarPortariaAsync(int id)
        {
            var p = await _repository.GetByIdAsync(id);
            if (p == null) return null;

            return new PortariaDTO
            {
                IdPortaria = p.IdPortaria,
                NomePorteiro = p.NomePorteiro,
                Turno = p.Turno,
                Contato = p.Contato,
                DataRegistro = p.DataRegistro
            };
        }

        public async Task CriarPortariaAsync(PortariaDTO dto)
        {
            var portaria = new Portaria
            {
                NomePorteiro = dto.NomePorteiro,
                Turno = dto.Turno,
                Contato = dto.Contato,
                DataRegistro = dto.DataRegistro
            };

            await _repository.AddAsync(portaria);
        }

        public async Task AtualizarPortariaAsync(PortariaDTO dto)
        {
            var portaria = new Portaria
            {
                IdPortaria = dto.IdPortaria,
                NomePorteiro = dto.NomePorteiro,
                Turno = dto.Turno,
                Contato = dto.Contato,
                DataRegistro = dto.DataRegistro
            };

            await _repository.UpdateAsync(portaria);
        }

        public async Task RemoverPortariaAsync(int id) =>
            await _repository.DeleteAsync(id);
    }
}
