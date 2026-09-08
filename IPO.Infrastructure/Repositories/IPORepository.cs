using IPO.Application.Interfaces;
using IPO.Domain.Entities;
using IPO.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace IPO.Infrastructure.Repositories
{
    public class IPORepository : IIPORepository
    {
        private readonly ApplicationDbContext _context;

        public IPORepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Domain.Entities.IPO>> GetAllAsync()
        {
            return await _context.IPOs
                .ToListAsync();
        }

        public async Task<Domain.Entities.IPO?> GetByIdAsync(int id)
        {
            return await _context.IPOs
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<Domain.Entities.IPO>> GetUpcomingAsync()
        {
            return await _context.IPOs
                .ToListAsync();
        }

        public async Task<List<Domain.Entities.IPO>> GetOpenAsync()
        {
            return await _context.IPOs
                .ToListAsync();
        }

        public async Task<List<Domain.Entities.IPO>> GetClosedAsync()
        {
            return await _context.IPOs
                .ToListAsync();
        }

        public async Task AddAsync(Domain.Entities.IPO ipo)
        {
            await _context.IPOs.AddAsync(ipo);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Domain.Entities.IPO ipo)
        {
            _context.IPOs.Update(ipo);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var ipo = await _context.IPOs.FindAsync(id);

            if (ipo is null)
                return;

            _context.IPOs.Remove(ipo);

            await _context.SaveChangesAsync();
        }
    }
}
