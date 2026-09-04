using IPO.Application.Interfaces;
using IPO.Domain.Entities;
using IPO.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;


namespace IPO.Infrastructure.Repositories
{
    public class RegistrarRepository : IRegistrarRepository
    {
        private readonly ApplicationDbContext _context;

        public RegistrarRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Registrar>> GetAllAsync()
        {
            return await _context.Registrars
                .ToListAsync();
        }

        public async Task<Registrar?> GetByIdAsync(int id)
        {
            return await _context.Registrars
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<Registrar>> GetUpcomingAsync()
        {
            return await _context.Registrars
                .ToListAsync();
        }

        public async Task<List<Registrar>> GetOpenAsync()
        {
            return await _context.Registrars    
                .ToListAsync();
        }

        public async Task<List<Registrar>> GetClosedAsync()
        {
            return await _context.Registrars
                .ToListAsync();
        }

        public async Task AddAsync(Registrar registrar)
        {
            await _context.Registrars.AddAsync(registrar);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Registrar registrar)
        {
            _context.Registrars.Update(registrar);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var registrar = await _context.Registrars.FindAsync(id);

            if (registrar is null)
                return;

            _context.Registrars.Remove(registrar);

            await _context.SaveChangesAsync();
        }

      
    }
}
