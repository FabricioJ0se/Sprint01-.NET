using PortariaLight.Domain.Entities;
using PortariaLight.Domain.Repositories;
using System.Collections.Generic;
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

        public async Task<IEnumerable<Encomenda>> ListarEncomendasAsync() =>
            await _repository.GetAllAsync();

        public async Task<Encomenda> BuscarEncomendaAsync(int id) =>
            await _repository.GetByIdAsync(id);

        public async Task CriarEncomendaAsync(Encomenda encomenda) =>
            await _repository.AddAsync(encomenda);

        public async Task AtualizarEncomendaAsync(Encomenda encomenda) =>
            await _repository.UpdateAsync(encomenda);

        public async Task RemoverEncomendaAsync(int id) =>
            await _repository.DeleteAsync(id);
    }
}
