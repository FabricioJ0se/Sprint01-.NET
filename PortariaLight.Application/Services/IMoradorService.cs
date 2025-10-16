using PortariaLight.Application.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PortariaLight.Application.Services
{
    public interface IMoradorService
    {
        Task<IEnumerable<MoradorDTO>> ListarMoradoresAsync();
        Task<MoradorDTO?> BuscarMoradorAsync(int id);
        Task CriarMoradorAsync(MoradorDTO morador);
        Task AtualizarMoradorAsync(MoradorDTO morador);
        Task RemoverMoradorAsync(int id);
    }
}
