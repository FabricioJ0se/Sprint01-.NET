using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PortariaLight.Domain.Entities;
using PortariaLight.Domain.Repositories;
using PortariaLight.Infrastructure.Data;

namespace PortariaLight.Infrastructure.Repositories
{
    public class MoradorRepository : IMoradorRepository
    {
        private readonly AppDbContext _context;

        public MoradorRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Morador>> GetAllAsync()
        {
            return await _context.Moradores.ToListAsync();
        }

        public async Task<Morador?> GetByIdAsync(int id)
        {
            return await _context.Moradores.FindAsync(id);
        }

        public async Task<Morador> CreateAsync(Morador morador)
        {
            _context.Moradores.Add(morador);
            await _context.SaveChangesAsync();
            return morador;
        }

        public async Task<Morador> UpdateAsync(Morador morador)
        {
            _context.Moradores.Update(morador);
            await _context.SaveChangesAsync();
            return morador;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var morador = await _context.Moradores.FindAsync(id);
            if (morador == null)
                return false;

            _context.Moradores.Remove(morador);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Morador>> GetByApartamentoIdAsync(int apartamentoId)
        {
            return await _context.Moradores
                .Where(m => m.IdApartamento == apartamentoId)
                .ToListAsync();
        }

        public async Task<Morador?> GetByContatoAsync(string contato)
        {
            return await _context.Moradores
                .FirstOrDefaultAsync(m => m.Contato == contato);
        }
    }
}