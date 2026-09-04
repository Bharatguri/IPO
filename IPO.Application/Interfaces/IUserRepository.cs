using IPO.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace IPO.Application.Interfaces
{

    public interface IUserRepository
    {
        Task<List<User>> GetAllAsync();
        Task<User?> GetByIdAsync(int id);

        Task AddAsync(User entity);
        Task UpdateAsync(User entity);
        Task DeleteAsync(int id);
    }
}
