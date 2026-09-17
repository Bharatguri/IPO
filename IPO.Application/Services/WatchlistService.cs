using IPO.Application.Interfaces;
using IPO.Application.Interfaces.Services;
using IPO.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace IPO.Application.Services
{
    public class WatchlistService : IWatchlistService
    {
        private readonly IWatchlistRepository _watchlistRepository;

        public WatchlistService(IWatchlistRepository watchlistRepository)
        {
            _watchlistRepository = watchlistRepository;
        }

        public async Task<List<Watchlist>> GetAllAsync()
        {
            return await _watchlistRepository.GetAllAsync();
        }

        public async Task<Watchlist?> GetByIdAsync(int id)
        {
            return await _watchlistRepository.GetByIdAsync(id);
        }

        public async Task AddAsync(Watchlist entity)
        {
            await _watchlistRepository.AddAsync(entity);
        }

        public async Task UpdateAsync(Watchlist entity)
        {
            await _watchlistRepository.UpdateAsync(entity);
        }

        public async Task DeleteAsync(int id)
        {
            await _watchlistRepository.DeleteAsync(id);
        }
    }
}
