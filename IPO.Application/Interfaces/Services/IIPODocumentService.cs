using IPO.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace IPO.Application.Interfaces.Services
{
    public interface IIPODocumentService
    {
        Task<List<IPODocument>> GetAllAsync();
        Task<IPODocument?> GetByIdAsync(int id);

        Task AddAsync(IPODocument entity);
        Task UpdateAsync(IPODocument entity);
        Task DeleteAsync(int id);
    }
}
