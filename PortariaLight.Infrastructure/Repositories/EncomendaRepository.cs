using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PortariaLight.Domain.Entities;
using PortariaLight.Domain.Repositories;
using PortariaLight.Infrastructure.Data;

namespace PortariaLight.Infrastructure.Repositories
{
    public class EncomendaRepository : IEncomendaRepository
    {
        private readonly AppDbContext _context;

        public EncomendaRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Encomenda>> GetAllAsync()
        {
            return await _context.Encomendas.ToListAsync();
        }

        public async Task<Encomenda?> GetByIdAsync(int id)
        {
            return await _context.Encomendas.FindAsync(id);
        }

        public async Task<Encomenda> CreateAsync(Encomenda encomenda)
        {
            _context.Encomendas.Add(encomenda);
            await _context.SaveChangesAsync();
            return encomenda;
        }

        public async Task<Encomenda> UpdateAsync(Encomenda encomenda)
        {
            _context.Encomendas.Update(encomenda);
            await _context.SaveChangesAsync();
            return encomenda;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var encomenda = await _context.Encomendas.FindAsync(id);
            if (encomenda == null)
                return false;

            _context.Encomendas.Remove(encomenda);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Encomenda>> GetByMoradorIdAsync(int moradorId)
        {
            return await _context.Encomendas
                .Where(e => e.IdMorador == moradorId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Encomenda>> GetNaoRetiradasAsync()
        {
            return await _context.Encomendas
                .Where(e => e.IdRetirada == 0 || e.IdRetirada == 0)
                .ToListAsync();
        }
    }
}