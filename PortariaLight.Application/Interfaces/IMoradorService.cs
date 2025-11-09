using PortariaLight.Domain.Entities;

namespace PortariaLight.Application.Services
{
    public interface IMoradorService
    {
        Task<IEnumerable<Morador>> GetAllMoradoresAsync();
        Task<Morador?> GetMoradorByIdAsync(int id);
        Task<Morador> CreateMoradorAsync(Morador morador);
        Task<Morador> UpdateMoradorAsync(Morador morador);
        Task<bool> DeleteMoradorAsync(int id);
        Task<IEnumerable<Morador>> GetMoradoresByApartamentoAsync(int apartamentoId);
        Task<Morador?> GetMoradorByContatoAsync(string contato);
    }
}