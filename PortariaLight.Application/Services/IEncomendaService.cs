using PortariaLight.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PortariaLight.Application.Services
{
    public interface IEncomendaService
    {
        Task<IEnumerable<Encomenda>> ListarEncomendasAsync();
        Task<Encomenda> BuscarEncomendaAsync(int id);
        Task CriarEncomendaAsync(Encomenda encomenda);
        Task AtualizarEncomendaAsync(Encomenda encomenda);
        Task RemoverEncomendaAsync(int id);
    }
}
