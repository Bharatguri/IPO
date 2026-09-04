using IPO.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace IPO.Application.Interfaces
{
    public interface IAllotmentRepository
    {
        Task<List<Allotment>> GetAllAsync();
        Task<Allotment?> GetByIdAsync(int id);
        Task AddAsync(Allotment entity);
        Task UpdateAsync(Allotment entity);
        Task DeleteAsync(int id);
    }
}
