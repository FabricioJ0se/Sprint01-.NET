using PortariaLight.Domain.Entities;
using PortariaLight.Domain.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

public class RetiradaService : IRetiradaService
{
    private readonly IRetiradaRepository _repository;

    public RetiradaService(IRetiradaRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<Retirada>> ListarRetiradasAsync() =>
        await _repository.GetAllAsync();

    public async Task<Retirada> BuscarRetiradaAsync(int id) =>
        await _repository.GetByIdAsync(id);

    public async Task CriarRetiradaAsync(Retirada retirada) =>
        await _repository.AddAsync(retirada);

    public async Task AtualizarRetiradaAsync(Retirada retirada) =>
        await _repository.UpdateAsync(retirada);

    public async Task RemoverRetiradaAsync(int id) =>
        await _repository.DeleteAsync(id);
}