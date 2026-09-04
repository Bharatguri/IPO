using IPO.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace IPO.Application.Interfaces
{
    public interface ISubscriptionRepository
    {
        Task<List<Subscription>> GetAllAsync();
        Task<Subscription?> GetByIdAsync(int id);
        Task<List<Subscription>> GetByIpoIdAsync(int ipoId);
        Task<List<Subscription>> GetByIpoAndDateAsync(int ipoId,DateTime date);
        Task AddAsync(Subscription entity);
        Task UpdateAsync(Subscription entity);
        Task DeleteAsync(int id);
    }
}
