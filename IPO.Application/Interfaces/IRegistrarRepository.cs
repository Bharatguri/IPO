using IPO.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace IPO.Application.Interfaces
{
    public interface IRegistrarRepository
    {
        Task<List<Registrar>> GetAllAsync();
        Task<Registrar?> GetByIdAsync(int id);

        Task AddAsync(Registrar entity);
        Task UpdateAsync(Registrar entity);
        Task DeleteAsync(int id);
    }
}
