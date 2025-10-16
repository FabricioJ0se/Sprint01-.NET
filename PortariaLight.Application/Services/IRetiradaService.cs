using PortariaLight.Application.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PortariaLight.Application.Services
{
    public interface IRetiradaService
    {
        Task<IEnumerable<RetiradaDTO>> ListarRetiradasAsync();
        Task<RetiradaDTO> BuscarRetiradaAsync(int id);
        Task CriarRetiradaAsync(RetiradaDTO dto);
        Task AtualizarRetiradaAsync(RetiradaDTO dto);
        Task RemoverRetiradaAsync(int id);
    }
}
