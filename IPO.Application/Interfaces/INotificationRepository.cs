using IPO.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace IPO.Application.Interfaces
{
    public interface INotificationRepository
    {
        Task<List<Notification>> GetAllAsync();
        Task<Notification?> GetByIdAsync(int id);

        Task AddAsync(Notification entity);
        Task UpdateAsync(Notification entity);
        Task DeleteAsync(int id);
    }
}
