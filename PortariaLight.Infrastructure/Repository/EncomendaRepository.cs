using PortariaLight.Domain.Entities;
using PortariaLight.Domain.Repositories; 
using PortariaLight.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PortariaLight.Infrastructure.Repositories 
{
    public class EncomendaRepository : IEncomendaRepository
    {
        private readonly AppDbContext _context;

        public EncomendaRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Encomenda>> GetAllAsync() =>
            await _context.Encomendas.ToListAsync();

        public async Task<Encomenda> GetByIdAsync(int id) =>
            await _context.Encomendas.FindAsync(id);

        public async Task AddAsync(Encomenda encomenda)
        {
            _context.Encomendas.Add(encomenda);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Encomenda encomenda)
        {
            _context.Encomendas.Update(encomenda);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var encomenda = await _context.Encomendas.FindAsync(id);
            if (encomenda != null)
            {
                _context.Encomendas.Remove(encomenda);
                await _context.SaveChangesAsync();
            }
        }
    }
}
