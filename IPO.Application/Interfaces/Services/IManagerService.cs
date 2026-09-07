using IPO.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace IPO.Application.Interfaces.Services
{
    public interface IManagerService
    {
        Task<List<Manager>> GetAllAsync();
        Task<Manager?> GetByIdAsync(int id);

        Task AddAsync(Manager entity);
        Task UpdateAsync(Manager entity);
        Task DeleteAsync(int id);
    }
}
