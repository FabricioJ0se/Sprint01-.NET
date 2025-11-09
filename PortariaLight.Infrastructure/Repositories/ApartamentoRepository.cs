using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PortariaLight.Domain.Entities;
using PortariaLight.Domain.Repositories;
using PortariaLight.Infrastructure.Data;

namespace PortariaLight.Infrastructure.Repositories
{
    public class ApartamentoRepository : IApartamentoRepository
    {
        private readonly AppDbContext _context;

        public ApartamentoRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Apartamento>> GetAllAsync()
        {
            return await _context.Apartamentos.ToListAsync();
        }

        public async Task<Apartamento?> GetByIdAsync(int id)
        {
            return await _context.Apartamentos.FindAsync(id);
        }

        public async Task<Apartamento> CreateAsync(Apartamento apartamento)
        {
            _context.Apartamentos.Add(apartamento);
            await _context.SaveChangesAsync();
            return apartamento;
        }

        public async Task<Apartamento> UpdateAsync(Apartamento apartamento)
        {
            _context.Apartamentos.Update(apartamento);
            await _context.SaveChangesAsync();
            return apartamento;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var apartamento = await _context.Apartamentos.FindAsync(id);
            if (apartamento == null)
                return false;

            _context.Apartamentos.Remove(apartamento);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}