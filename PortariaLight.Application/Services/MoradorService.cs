using PortariaLight.Domain.Entities;
using PortariaLight.Domain.Repositories;
using System.Collections.Generic;
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

        public async Task<IEnumerable<Morador>> ListarMoradoresAsync() =>
            await _repository.GetAllAsync();

        public async Task<Morador> BuscarMoradorAsync(int id) =>
            await _repository.GetByIdAsync(id);

        public async Task CriarMoradorAsync(Morador morador) =>
            await _repository.AddAsync(morador);

        public async Task AtualizarMoradorAsync(Morador morador) =>
            await _repository.UpdateAsync(morador);

        public async Task RemoverMoradorAsync(int id) =>
            await _repository.DeleteAsync(id);
    }
}
