using IPO.Application.Interfaces;
using IPO.Domain.Entities;
using IPO.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace IPO.Infrastructure.Repositories
{
    public class IPOApplicationRepository : IIPOApplicationRepository
    {
        private readonly ApplicationDbContext _context;

        public IPOApplicationRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<IPOApplication>> GetAllAsync()
        {
            return await _context.IPOApplications
                .ToListAsync();
        }

        public async Task<IPOApplication?> GetByIdAsync(int id)
        {
            return await _context.IPOApplications
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<IPOApplication>> GetUpcomingAsync()
        {
            return await _context.IPOApplications
                .ToListAsync();
        }

        public async Task<List<IPOApplication>> GetOpenAsync()
        {
            return await _context.IPOApplications   
                .ToListAsync();
        }

        public async Task<List<IPOApplication>> GetClosedAsync()
        {
            return await _context.IPOApplications
                .ToListAsync();
        }

        public async Task AddAsync(IPOApplication application)
        {
            await _context.IPOApplications.AddAsync(application);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(IPOApplication application)
        {
            _context.IPOApplications.Update(application);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var application = await _context.IPOApplications.FindAsync(id);

            if (application is null)
                return;

            _context.IPOApplications.Remove(application);

            await _context.SaveChangesAsync();
        }

    }
}
