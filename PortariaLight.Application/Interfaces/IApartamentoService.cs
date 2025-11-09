using PortariaLight.Domain.Entities;

namespace PortariaLight.Application.Services
{
    public interface IApartamentoService
    {
        Task<IEnumerable<Apartamento>> GetAllApartamentosAsync();
        Task<Apartamento?> GetApartamentoByIdAsync(int id);
        Task<Apartamento> CreateApartamentoAsync(Apartamento apartamento);
        Task<Apartamento> UpdateApartamentoAsync(Apartamento apartamento);
        Task<bool> DeleteApartamentoAsync(int id);
    }
}