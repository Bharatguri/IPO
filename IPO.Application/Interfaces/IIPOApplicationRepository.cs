using IPO.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace IPO.Application.Interfaces
{
    public interface IIPOApplicationRepository
    {
        Task<List<IPOApplication>> GetAllAsync();
        Task<IPOApplication?> GetByIdAsync(int id);

        Task AddAsync(IPOApplication entity);
        Task UpdateAsync(IPOApplication entity);
        Task DeleteAsync(int id);
    }
}
