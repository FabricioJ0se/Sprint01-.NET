using PortariaLight.Domain.Entities;
using PortariaLight.Domain.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

public class PortariaService : IPortariaService
{
    private readonly IPortariaRepository _repository;

    public PortariaService(IPortariaRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<Portaria>> ListarPortariasAsync() =>
        await _repository.GetAllAsync();

    public async Task<Portaria> BuscarPortariaAsync(int id) =>
        await _repository.GetByIdAsync(id);

    public async Task CriarPortariaAsync(Portaria portaria) =>
        await _repository.AddAsync(portaria);

    public async Task AtualizarPortariaAsync(Portaria portaria) =>
        await _repository.UpdateAsync(portaria);

    public async Task RemoverPortariaAsync(int id) =>
        await _repository.DeleteAsync(id);
}