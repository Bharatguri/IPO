using IPO.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace IPO.Application.Interfaces
{
    public interface IIPORepository
    {
        Task<List<IPOs>> GetAllAsync();
        Task<IPOs?> GetByIdAsync(int id);
        Task<List<IPOs>> GetUpcomingAsync();
        Task<List<IPOs>> GetOpenAsync();
        Task<List<IPOs>> GetClosedAsync();
        Task AddAsync(IPOs ipo);
        Task UpdateAsync(IPOs ipo);
        Task DeleteAsync(int id);
    }
}
