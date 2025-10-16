using PortariaLight.Application.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PortariaLight.Application.Services
{
    public interface IPortariaService
    {
        Task<IEnumerable<PortariaDTO>> ListarPortariasAsync();
        Task<PortariaDTO> BuscarPortariaAsync(int id);
        Task CriarPortariaAsync(PortariaDTO dto);
        Task AtualizarPortariaAsync(PortariaDTO dto);
        Task RemoverPortariaAsync(int id);
    }
}
