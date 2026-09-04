using IPO.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace IPO.Application.Interfaces
{
    public interface IIPODocumentRepository
    {
        Task<List<IPODocument>> GetAllAsync();
        Task<IPODocument?> GetByIdAsync(int id);

        Task AddAsync(IPODocument entity);
        Task UpdateAsync(IPODocument entity);
        Task DeleteAsync(int id);
    }
}
