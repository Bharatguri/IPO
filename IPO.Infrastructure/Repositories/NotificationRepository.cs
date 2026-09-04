using IPO.Application.Interfaces;
using IPO.Domain.Entities;
using IPO.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace IPO.Infrastructure.Repositories
{
    public class NotificationRepository : INotificationRepository
    {
        private readonly ApplicationDbContext _context;

        public NotificationRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Notification>> GetAllAsync()
        {
            return await _context.Notifications
                .ToListAsync();
        }

        public async Task<Notification?> GetByIdAsync(int id)
        {
            return await _context.Notifications
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<Notification>> GetUpcomingAsync()
        {
            return await _context.Notifications
                .ToListAsync();
        }

        public async Task<List<Notification>> GetOpenAsync()
        {
            return await _context.Notifications
                .ToListAsync();
        }

        public async Task<List<Notification>> GetClosedAsync()
        {
            return await _context.Notifications
                .ToListAsync();
        }

        public async Task AddAsync(Notification notification)
        {
            await _context.Notifications.AddAsync(notification);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Notification notification)
        {
            _context.Notifications.Update(notification);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var notification = await _context.Notifications.FindAsync(id);

            if (notification is null)
                return;

            _context.Notifications.Remove(notification);

            await _context.SaveChangesAsync();
        }

    }
}
