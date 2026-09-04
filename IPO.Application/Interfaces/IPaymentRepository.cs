using IPO.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace IPO.Application.Interfaces
{
    public interface IPaymentRepository
    {
        Task<List<Payments>> GetAllAsync();
        Task<Payments?> GetByIdAsync(int id);

        Task AddAsync(Payments entity);
        Task UpdateAsync(Payments entity);
        Task DeleteAsync(int id);
    }
}
