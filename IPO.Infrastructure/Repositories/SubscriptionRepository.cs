using IPO.Application.Interfaces;
using IPO.Domain.Entities;
using IPO.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace IPO.Infrastructure.Repositories
{
    public class SubscriptionRepository : ISubscriptionRepository
    {
        private readonly ApplicationDbContext _context;

        public SubscriptionRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Subscription>> GetAllAsync()
        {
            return await _context.Subscriptions
                .Include(x => x.IPOId)
                .OrderByDescending(x => x.SubscriptionDate)
                .ToListAsync();
        }

        public async Task<Subscription?> GetByIdAsync(int id)
        {
            return await _context.Subscriptions
                .Include(x => x.IPOId)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<Subscription>> GetByIpoIdAsync(int ipoId)
        {
            return await _context.Subscriptions
                .Where(x => x.IPOId == ipoId)
                .OrderBy(x => x.SubscriptionDate)
                .ThenBy(x => x.Day)
                .ToListAsync();
        }

        public async Task<List<Subscription>> GetByIpoAndDateAsync(
            int ipoId,
            DateTime date)
        {
            return await _context.Subscriptions
                .Where(x =>
                    x.IPOId == ipoId &&
                    x.SubscriptionDate.Date == date.Date)
                .OrderBy(x => x.Category)
                .ToListAsync();
        }

        public async Task AddAsync(Subscription entity)
        {
            await _context.Subscriptions.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Subscription entity)
        {
            _context.Subscriptions.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var subscription = await _context.Subscriptions
                .FindAsync(id);

            if (subscription is null)
                return;

            _context.Subscriptions.Remove(subscription);

            await _context.SaveChangesAsync();
        }
    }
}
