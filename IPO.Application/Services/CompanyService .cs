using IPO.Application.DTOs;
using IPO.Application.Interfaces;
using IPO.Application.Interfaces.Services;
using IPO.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace IPO.Application.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly ICompanyRepository _repository;

        public CompanyService(ICompanyRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<CompanyListDto>> GetAllAsync()
        {
            var companies = await _repository.GetAllAsync();

            return companies.Select(MapToListDto).ToList();
        }

        public async Task<CompanyListDto?> GetByIdAsync(int id)
        {
            var company = await _repository.GetByIdAsync(id);

            if (company is null)
                return null;

            return MapToDetailDto(company);
        }

        public async Task<CompanyListDto> CreateAsync(
            CompanyListDto request)
        {
            var company = new Company
            {
                Name = request.Name,
                LogoUrl = request.LogoUrl,
                About = request.About,
                WebsiteUrl = request.WebsiteUrl,
                Address = request.Address,
                IsActive = request.IsActive
            };

            await _repository.AddAsync(company);

            return MapToDetailDto(company);
        }

        public async Task<bool> UpdateAsync(
            int id,
            CompanyListDto request)
        {
            var company = await _repository.GetByIdAsync(id);

            if (company is null)
                return false;

            company.Name = request.Name;
            company.LogoUrl = request.LogoUrl;
            company.About = request.About;
            company.WebsiteUrl = request.WebsiteUrl;
            company.Address = request.Address;
            company.IsActive = request.IsActive;

            await _repository.UpdateAsync(company);

            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var company = await _repository.GetByIdAsync(id);

            if (company is null)
                return false;

            await _repository.DeleteAsync(id);

            return true;
        }

        private static CompanyListDto MapToListDto(
            Company company)
        {
            return new CompanyListDto
            {
                Id = company.Id,
                Name = company.Name,
                LogoUrl = company.LogoUrl,
                IsActive = company.IsActive
            };
        }

        private static CompanyListDto MapToDetailDto(
            Company company)
        {
            return new CompanyListDto
            {
                Id = company.Id,
                Name = company.Name,
                LogoUrl = company.LogoUrl,
                About = company.About,
                WebsiteUrl = company.WebsiteUrl,
                Address = company.Address,
                IsActive = company.IsActive
            };
        }
    }
}
