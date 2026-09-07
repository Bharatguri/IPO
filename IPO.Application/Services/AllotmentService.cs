using IPO.Application.Interfaces;
using IPO.Application.Interfaces.Services;
using IPO.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace IPO.Application.Services
{
    public class AllotmentService : IAllotmentService
    {
        private readonly IAllotmentRepository _allotmentRepository;

        public AllotmentService(IAllotmentRepository allotmentRepository)
        {
            _allotmentRepository = allotmentRepository;
        }

        public async Task<List<Allotment>> GetAllAsync()
        {
            return await _allotmentRepository.GetAllAsync();
        }

        public async Task<Allotment?> GetByIdAsync(int id)
        {
            return await _allotmentRepository.GetByIdAsync(id);
        }

        public async Task AddAsync(Allotment entity)
        {
            await _allotmentRepository.AddAsync(entity);
        }

        public async Task UpdateAsync(Allotment entity)
        {
            await _allotmentRepository.UpdateAsync(entity);
        }

        public async Task DeleteAsync(int id)
        {
            await _allotmentRepository.DeleteAsync(id);
        }
    }
}
