using IPO.Application.Interfaces;
using IPO.Domain.Entities;
using IPO.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;


namespace IPO.Infrastructure.Repositories
{
    public class WatchlistRepository : IWatchlistRepository
    {
        private readonly ApplicationDbContext _context;

        public WatchlistRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Watchlist>> GetAllAsync()
        {
            return await _context.Watchlists
                .ToListAsync();
        }

        public async Task<Watchlist?> GetByIdAsync(int id)
        {
            return await _context.Watchlists
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<Watchlist>> GetUpcomingAsync()
        {
            return await _context.Watchlists
                .ToListAsync();
        }

        public async Task<List<Watchlist>> GetOpenAsync()
        {
            return await _context.Watchlists
                .ToListAsync();
        }

        public async Task<List<Watchlist>> GetClosedAsync()
        {
            return await _context.Watchlists
                .ToListAsync();
        }

        public async Task AddAsync(Watchlist watchlist)
        {
            await _context.Watchlists.AddAsync(watchlist);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Watchlist watchlist)
        {
            _context.Watchlists.Update(watchlist);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var watchlist = await _context.Watchlists.FindAsync(id);

            if (watchlist is null)
                return;

            _context.Watchlists.Remove(watchlist);

            await _context.SaveChangesAsync();
        }

    }
}
