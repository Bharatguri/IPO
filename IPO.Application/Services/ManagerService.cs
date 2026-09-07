using IPO.Application.Interfaces;
using IPO.Application.Interfaces.Services;
using IPO.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace IPO.Application.Services
{
    public class ManagerService : IManagerService
    {
        private readonly IManagerRepository _managerRepository;

        public ManagerService(IManagerRepository managerRepository)
        {
            _managerRepository = managerRepository;
        }

        public async Task<List<Manager>> GetAllAsync()
        {
            return await _managerRepository.GetAllAsync();
        }

        public async Task<Manager?> GetByIdAsync(int id)
        {
            return await _managerRepository.GetByIdAsync(id);
        }

        public async Task AddAsync(Manager entity)
        {
            await _managerRepository.AddAsync(entity);
        }

        public async Task UpdateAsync(Manager entity)
        {
            await _managerRepository.UpdateAsync(entity);
        }

        public async Task DeleteAsync(int id)
        {
            await _managerRepository.DeleteAsync(id);
        }
    }
}