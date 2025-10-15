using PortariaLight.Domain.Entities;
using PortariaLight.Domain.Repositories;
using PortariaLight.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PortariaLight.Infrastructure.Repositories
{
    public class MoradorRepository : IMoradorRepository
    {
        private readonly AppDbContext _context;

        public MoradorRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Morador>> GetAllAsync() =>
            await _context.Moradores.ToListAsync();

        public async Task<Morador> GetByIdAsync(int id) =>
            await _context.Moradores.FindAsync(id);

        public async Task AddAsync(Morador morador)
        {
            _context.Moradores.Add(morador);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Morador morador)
        {
            _context.Moradores.Update(morador);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var morador = await _context.Moradores.FindAsync(id);
            if (morador != null)
            {
                _context.Moradores.Remove(morador);
                await _context.SaveChangesAsync();
            }
        }
    }
}
