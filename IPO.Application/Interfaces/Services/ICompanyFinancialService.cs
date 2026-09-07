using IPO.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace IPO.Application.Interfaces.Services
{
    public interface ICompanyFinancialService
    {
        Task<List<CompanyFinancial>> GetAllAsync();
        Task<CompanyFinancial?> GetByIdAsync(int id);

        //Task<List<CompanyFinancial>> GetUpcomingAsync();
        //Task<List<CompanyFinancial>> GetOpenAsync();
        //Task<List<CompanyFinancial>> GetClosedAsync();

        Task AddAsync(CompanyFinancial entity);
        Task UpdateAsync(CompanyFinancial entity);
        Task DeleteAsync(int id);
    }
}
