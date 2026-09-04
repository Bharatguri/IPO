using IPO.Application.Interfaces;
using IPO.Domain.Entities;
using IPO.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace IPO.Infrastructure.Repositories
{
    public class AllotmentRepository : IAllotmentRepository
    {
        private readonly ApplicationDbContext _context;

        public AllotmentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Allotment>> GetAllAsync()
        {
            return await _context.Allotments
                .ToListAsync();
        }

        public async Task<Allotment?> GetByIdAsync(int id)
        {
            return await _context.Allotments
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<Allotment>> GetUpcomingAsync()
        {
            return await _context.Allotments
                .ToListAsync();
        }

        public async Task<List<Allotment>> GetOpenAsync()
        {
            return await _context.Allotments
                .ToListAsync();
        }

        public async Task<List<Allotment>> GetClosedAsync()
        {
            return await _context.Allotments
                .ToListAsync();
        }

        public async Task AddAsync(Allotment allotment)
        {
            await _context.Allotments.AddAsync(allotment);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Allotment allotment)
        {
            _context.Allotments.Update(allotment);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var allotment = await _context.Allotments.FindAsync(id);

            if (allotment is null)
                return;

            _context.Allotments.Remove(allotment);

            await _context.SaveChangesAsync();
        }

    }
}
