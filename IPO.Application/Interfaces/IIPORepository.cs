using IPO.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace IPO.Application.Interfaces
{
    public interface IIPORepository
    {
        Task<List<Domain.Entities.IPO>> GetAllAsync();
        Task<Domain.Entities.IPO?> GetByIdAsync(int id);
        Task<List<Domain.Entities.IPO>> GetUpcomingAsync();
        Task<List<Domain.Entities.IPO>> GetOpenAsync();
        Task<List<Domain.Entities.IPO>> GetClosedAsync();
        Task AddAsync(Domain.Entities.IPO ipo);
        Task UpdateAsync(Domain.Entities.IPO ipo);
        Task DeleteAsync(int id);
    }
}
