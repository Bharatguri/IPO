using IPO.Application.Interfaces;
using IPO.Application.Interfaces.Services;
using IPO.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace IPO.Application.Services
{
    public class SubscriptionService : ISubscriptionService
    {
        private readonly ISubscriptionRepository _repository;

        public SubscriptionService(
            ISubscriptionRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<Subscription>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Subscription?> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<List<Subscription>> GetByIpoIdAsync(int ipoId)
        {
            return await _repository.GetByIpoIdAsync(ipoId);
        }

        public async Task<List<Subscription>> GetByIpoAndDateAsync(
            int ipoId,
            DateTime date)
        {
            return await _repository.GetByIpoAndDateAsync(ipoId, date);
        }

        public async Task AddAsync(Subscription entity)
        {
            await _repository.AddAsync(entity);
        }

        public async Task UpdateAsync(Subscription entity)
        {
            await _repository.UpdateAsync(entity);
        }

        public async Task DeleteAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }
    }
}
