using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PortariaLight.Domain.Entities;
using PortariaLight.Domain.Repositories;
using PortariaLight.Infrastructure.Data;

namespace PortariaLight.Infrastructure.Repositories
{
    public class RetiradaRepository : IRetiradaRepository
    {
        private readonly AppDbContext _context;

        public RetiradaRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Retirada>> GetAllAsync()
        {
            return await _context.Retiradas.ToListAsync();
        }

        public async Task<Retirada?> GetByIdAsync(int id)
        {
            return await _context.Retiradas.FindAsync(id);
        }

        public async Task<Retirada> CreateAsync(Retirada retirada)
        {
            _context.Retiradas.Add(retirada);
            await _context.SaveChangesAsync();
            return retirada;
        }

        public async Task<Retirada> UpdateAsync(Retirada retirada)
        {
            _context.Retiradas.Update(retirada);
            await _context.SaveChangesAsync();
            return retirada;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var retirada = await _context.Retiradas.FindAsync(id);
            if (retirada == null)
                return false;

            _context.Retiradas.Remove(retirada);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Retirada>> GetByEncomendaIdAsync(int encomendaId)
        {
            return await _context.Retiradas
                .Where(r => r.IdRetirada == encomendaId) 
                .ToListAsync();
        }

        public async Task<IEnumerable<Retirada>> GetByMoradorIdAsync(int moradorId)
        {
            return await _context.Retiradas
                .Where(r => r.IdMorador == moradorId)
                .ToListAsync();
        }
    }
}