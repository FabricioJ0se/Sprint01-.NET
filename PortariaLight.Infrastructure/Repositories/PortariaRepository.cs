using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PortariaLight.Domain.Entities;
using PortariaLight.Domain.Repositories;
using PortariaLight.Infrastructure.Data;

namespace PortariaLight.Infrastructure.Repositories
{
    public class PortariaRepository : IPortariaRepository
    {
        private readonly AppDbContext _context;

        public PortariaRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Portaria>> GetAllAsync()
        {
            return await _context.Portarias.ToListAsync();
        }

        public async Task<Portaria?> GetByIdAsync(int id)
        {
            return await _context.Portarias.FindAsync(id);
        }

        public async Task<Portaria> CreateAsync(Portaria portaria)
        {
            _context.Portarias.Add(portaria);
            await _context.SaveChangesAsync();
            return portaria;
        }

        public async Task<Portaria> UpdateAsync(Portaria portaria)
        {
            _context.Portarias.Update(portaria);
            await _context.SaveChangesAsync();
            return portaria;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var portaria = await _context.Portarias.FindAsync(id);
            if (portaria == null)
                return false;

            _context.Portarias.Remove(portaria);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}