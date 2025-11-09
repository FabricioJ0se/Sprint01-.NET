using PortariaLight.Domain.Entities;

namespace PortariaLight.Application.Services
{
    public interface IRetiradaService
    {
        Task<IEnumerable<Retirada>> GetAllRetiradasAsync();
        Task<Retirada?> GetRetiradaByIdAsync(int id);
        Task<Retirada> CreateRetiradaAsync(Retirada retirada);
        Task<Retirada> UpdateRetiradaAsync(Retirada retirada);
        Task<bool> DeleteRetiradaAsync(int id);
        Task<IEnumerable<Retirada>> GetRetiradasByEncomendaAsync(int encomendaId);
        Task<IEnumerable<Retirada>> GetRetiradasByMoradorAsync(int moradorId);
    }
}