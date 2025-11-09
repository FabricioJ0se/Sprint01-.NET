using PortariaLight.Domain.Entities;

namespace PortariaLight.Domain.Repositories
{
    public interface IMoradorRepository
    {
        Task<IEnumerable<Morador>> GetAllAsync();
        Task<Morador?> GetByIdAsync(int id);
        Task<Morador> CreateAsync(Morador morador);
        Task<Morador> UpdateAsync(Morador morador);
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<Morador>> GetByApartamentoIdAsync(int apartamentoId);
        Task<Morador?> GetByContatoAsync(string contato);
    }
}