using PortariaLight.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PortariaLight.Application.Services
{
    public interface IMoradorService
    {
        Task<IEnumerable<Morador>> ListarMoradoresAsync();
        Task<Morador> BuscarMoradorAsync(int id);
        Task CriarMoradorAsync(Morador morador);
        Task AtualizarMoradorAsync(Morador morador);
        Task RemoverMoradorAsync(int id);
    }
}
