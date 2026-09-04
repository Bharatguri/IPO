using IPO.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace IPO.Application.Interfaces
{
    public interface IManagerRepository
    {
        Task<List<Manager>> GetAllAsync();
        Task<Manager?> GetByIdAsync(int id);

        Task AddAsync(Manager entity);
        Task UpdateAsync(Manager entity);
        Task DeleteAsync(int id);
    }
}
