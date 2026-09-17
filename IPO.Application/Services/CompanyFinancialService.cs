using IPO.Application.Interfaces;
using IPO.Application.Interfaces.Services;
using IPO.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace IPO.Application.Services
{
    public class CompanyFinancialService : ICompanyFinancialService
    {
        private readonly ICompanyFinancialRepository _companyFinancialRepository;

        public CompanyFinancialService(
            ICompanyFinancialRepository companyFinancialRepository)
        {
            _companyFinancialRepository = companyFinancialRepository;
        }

        public async Task<List<CompanyFinancial>> GetAllAsync()
        {
            return await _companyFinancialRepository.GetAllAsync();
        }

        public async Task<CompanyFinancial?> GetByIdAsync(int id)
        {
            return await _companyFinancialRepository.GetByIdAsync(id);
        }

        //public async Task<List<CompanyFinancial>> GetUpcomingAsync()
        //{
        //    return await _companyFinancialRepository.GetUpcomingAsync();
        //}

        //public async Task<List<CompanyFinancial>> GetOpenAsync()
        //{
        //    return await _companyFinancialRepository.GetOpenAsync();
        //}

        //public async Task<List<CompanyFinancial>> GetClosedAsync()
        //{
        //    return await _companyFinancialRepository.GetClosedAsync();
        //}

        public async Task AddAsync(CompanyFinancial entity)
        {
            await _companyFinancialRepository.AddAsync(entity);
        }

        public async Task UpdateAsync(CompanyFinancial entity)
        {
            await _companyFinancialRepository.UpdateAsync(entity);
        }

        public async Task DeleteAsync(int id)
        {
            await _companyFinancialRepository.DeleteAsync(id);
        }
    }
}
