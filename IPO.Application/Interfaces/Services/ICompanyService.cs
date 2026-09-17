using IPO.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace IPO.Application.Interfaces.Services
{
    public interface ICompanyService
    {
        Task<List<CompanyListDto>> GetAllAsync();
        Task<CompanyListDto?> GetByIdAsync(int id);
        Task<CompanyListDto> CreateAsync(CompanyListDto request);
        Task<bool> UpdateAsync(int id, CompanyListDto request);
        Task<bool> DeleteAsync(int id);
    }
}
