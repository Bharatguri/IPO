using IPO.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace IPO.Application.Interfaces.Services
{
    public interface IWatchlistService
    {
        Task<List<Watchlist>> GetAllAsync();
        Task<Watchlist?> GetByIdAsync(int id);

        Task AddAsync(Watchlist entity);
        Task UpdateAsync(Watchlist entity);
        Task DeleteAsync(int id);
    }
}
