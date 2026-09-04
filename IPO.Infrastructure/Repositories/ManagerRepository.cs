using IPO.Application.Interfaces;
using IPO.Domain.Entities;
using IPO.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace IPO.Infrastructure.Repositories
{
    public class ManagerRepository : IManagerRepository    
    {
        private readonly ApplicationDbContext _context;

        public ManagerRepository(ApplicationDbContext context)
        { 
            _context = context;
        }

        public async Task<List<Manager>> GetAllAsync()
        {
            return await _context.Managers
                .ToListAsync();
        }

        public async Task<Manager?> GetByIdAsync(int id)
        {
            return await _context.Managers
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<Manager>> GetUpcomingAsync()
        {
            return await _context.Managers
                .ToListAsync();
        }

        public async Task<List<Manager>> GetOpenAsync()
        {
            return await _context.Managers
                .ToListAsync();
        }

        public async Task<List<Manager>> GetClosedAsync()
        {
            return await _context.Managers
                .ToListAsync();
        }

        public async Task AddAsync(Manager manager)
        {
            await _context.Managers.AddAsync(manager);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Manager manager)
        {
            _context.Managers.Update(manager);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var manager = await _context.Managers.FindAsync(id);

            if (manager is null)
                return;

            _context.Managers.Remove(manager);

            await _context.SaveChangesAsync();
        }

    }
}
