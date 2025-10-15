using PortariaLight.Domain.Entities;
using PortariaLight.Domain.Repositories; 
using PortariaLight.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PortariaLight.Infrastructure.Repositories 
{
    public class RetiradaRepository : IRetiradaRepository
    {
        private readonly AppDbContext _context;

        public RetiradaRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Retirada>> GetAllAsync() =>
            await _context.Retiradas.ToListAsync();

        public async Task<Retirada> GetByIdAsync(int id) =>
            await _context.Retiradas.FindAsync(id);

        public async Task AddAsync(Retirada retirada)
        {
            _context.Retiradas.Add(retirada);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Retirada retirada)
        {
            _context.Retiradas.Update(retirada);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var retirada = await _context.Retiradas.FindAsync(id);
            if (retirada != null)
            {
                _context.Retiradas.Remove(retirada);
                await _context.SaveChangesAsync();
            }
        }
    }
}
