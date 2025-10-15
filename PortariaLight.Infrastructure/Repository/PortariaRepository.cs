using PortariaLight.Domain.Entities;
using PortariaLight.Domain.Repositories;
using PortariaLight.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PortariaLight.Infrastructure.Repositories
{
    public class PortariaRepository : IPortariaRepository
    {
        private readonly AppDbContext _context;

        public PortariaRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Portaria>> GetAllAsync() =>
            await _context.Portarias.ToListAsync();

        public async Task<Portaria> GetByIdAsync(int id) =>
            await _context.Portarias.FindAsync(id);

        public async Task AddAsync(Portaria portaria)
        {
            _context.Portarias.Add(portaria);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Portaria portaria)
        {
            _context.Portarias.Update(portaria);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var portaria = await _context.Portarias.FindAsync(id);
            if (portaria != null)
            {
                _context.Portarias.Remove(portaria);
                await _context.SaveChangesAsync();
            }
        }
    }
}
