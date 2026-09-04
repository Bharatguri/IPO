using IPO.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace IPO.Application.Interfaces
{
    public interface ICompanyFinancialRepository
    {
        Task<List<CompanyFinancial>> GetAllAsync();
        Task<CompanyFinancial?> GetByIdAsync(int id);
        Task AddAsync(CompanyFinancial entity);
        Task UpdateAsync(CompanyFinancial entity);
        Task DeleteAsync(int id);
    }
}
