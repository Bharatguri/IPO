using IPO.Application.Interfaces;
using IPO.Application.Interfaces.Services;
using IPO.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace IPO.Application.Services
{
    public class IPODocumentService : IIPODocumentService
    {
        private readonly IIPODocumentRepository _ipoDocumentRepository;

        public IPODocumentService(
            IIPODocumentRepository ipoDocumentRepository)
        {
            _ipoDocumentRepository = ipoDocumentRepository;
        }

        public async Task<List<IPODocument>> GetAllAsync()
        {
            return await _ipoDocumentRepository.GetAllAsync();
        }

        public async Task<IPODocument?> GetByIdAsync(int id)
        {
            return await _ipoDocumentRepository.GetByIdAsync(id);
        }

        public async Task AddAsync(IPODocument entity)
        {
            await _ipoDocumentRepository.AddAsync(entity);
        }

        public async Task UpdateAsync(IPODocument entity)
        {
            await _ipoDocumentRepository.UpdateAsync(entity);
        }

        public async Task DeleteAsync(int id)
        {
            await _ipoDocumentRepository.DeleteAsync(id);
        }
    }
}
