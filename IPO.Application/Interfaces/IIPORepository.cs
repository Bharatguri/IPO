using System;using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using IPO.Domain.Entities;
using IPOEntity = IPO.Domain.Entities.IPO;

namespace IPO.Application.Interfaces
{
    public interface IIPORepository
    {
        Task<List<IPOEntity>> GetAllAsync();
        Task<IPOEntity?> GetByIdAsync(int id);
        Task<List<IPOEntity>> GetUpcomingAsync();
        Task<List<IPOEntity>> GetOpenAsync();
        Task<List<IPOEntity>> GetClosedAsync();
        Task AddAsync(IPOEntity ipo);
        Task UpdateAsync(IPOEntity ipo);
        Task DeleteAsync(int id);
    }
}
