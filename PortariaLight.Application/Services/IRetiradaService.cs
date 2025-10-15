using PortariaLight.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface IRetiradaService
{
    Task<IEnumerable<Retirada>> ListarRetiradasAsync();
    Task<Retirada> BuscarRetiradaAsync(int id);
    Task CriarRetiradaAsync(Retirada retirada);
    Task AtualizarRetiradaAsync(Retirada retirada);
    Task RemoverRetiradaAsync(int id);
}