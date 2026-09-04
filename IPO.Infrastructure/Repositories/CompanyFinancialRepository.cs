using IPO.Application.Interfaces;
using IPO.Domain.Entities;
using IPO.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace IPO.Infrastructure.Repositories
{
    public class CompanyFinancialRepository : ICompanyFinancialRepository
    {
        private readonly ApplicationDbContext _context;

        public CompanyFinancialRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<CompanyFinancial>> GetAllAsync()
        {
            return await _context.CompanyFinancials
                .ToListAsync();
        }

        public async Task<CompanyFinancial?> GetByIdAsync(int id)
        {
            return await _context.CompanyFinancials
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<CompanyFinancial>> GetUpcomingAsync()
        {
            return await _context.CompanyFinancials
                .ToListAsync();
        }

        public async Task<List<CompanyFinancial>> GetOpenAsync()
        {
            return await _context.CompanyFinancials
                .ToListAsync();
        }

        public async Task<List<CompanyFinancial>> GetClosedAsync()
        {
            return await _context.CompanyFinancials
                .ToListAsync();
        }

        public async Task AddAsync(CompanyFinancial companyFinancial)
        {
            await _context.CompanyFinancials.AddAsync(companyFinancial);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(CompanyFinancial companyFinancial)
        {
            _context.CompanyFinancials.Update(companyFinancial);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var companyFinancial = await _context.CompanyFinancials.FindAsync(id);

            if (companyFinancial is null)
                return;

            _context.CompanyFinancials.Remove(companyFinancial);

            await _context.SaveChangesAsync();
        }

    }
}
