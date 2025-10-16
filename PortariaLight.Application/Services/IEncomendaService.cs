using PortariaLight.Application.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PortariaLight.Application.Services
{
    public interface IEncomendaService
    {
        Task<IEnumerable<EncomendaDTO>> ListarEncomendasAsync();
        Task<EncomendaDTO?> BuscarEncomendaAsync(int id); 
        Task CriarEncomendaAsync(EncomendaDTO dto);
        Task AtualizarEncomendaAsync(EncomendaDTO dto);
        Task RemoverEncomendaAsync(int id);
    }
}
