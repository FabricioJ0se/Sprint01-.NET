using PortariaLight.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface IPortariaService
{
    Task<IEnumerable<Portaria>> ListarPortariasAsync();
    Task<Portaria> BuscarPortariaAsync(int id);
    Task CriarPortariaAsync(Portaria portaria);
    Task AtualizarPortariaAsync(Portaria portaria);
    Task RemoverPortariaAsync(int id);
}