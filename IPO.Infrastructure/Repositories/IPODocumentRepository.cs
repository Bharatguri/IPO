using IPO.Application.Interfaces;
using IPO.Domain.Entities;
using IPO.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace IPO.Infrastructure.Repositories
{
    public class IPODocumentRepository : IIPODocumentRepository
    {
        private readonly ApplicationDbContext _context;

        public IPODocumentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<IPODocument>> GetAllAsync()
        {
            return await _context.IPODocuments  
                .ToListAsync();
        }

        public async Task<IPODocument?> GetByIdAsync(int id)
        {
            return await _context.IPODocuments
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<IPODocument>> GetUpcomingAsync()
        {
            return await _context.IPODocuments
                .ToListAsync();
        }

        public async Task<List<IPODocument>> GetOpenAsync()
        {
            return await _context.IPODocuments
                .ToListAsync();
        }

        public async Task<List<IPODocument>> GetClosedAsync()
        {
            return await _context.IPODocuments
                .ToListAsync();
        }

        public async Task AddAsync(IPODocument document)
        {
            await _context.IPODocuments.AddAsync(document);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(IPODocument document)
        {
            _context.IPODocuments.Update(document);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var document = await _context.IPODocuments.FindAsync(id);

            if (document is null)
                return;

            _context.IPODocuments.Remove(document);

            await _context.SaveChangesAsync();
        }

    }
}
