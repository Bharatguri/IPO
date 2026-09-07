using IPO.Application.Interfaces;
using IPO.Application.Interfaces.Services;
using IPO.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace IPO.Application.Services
{
    public class RegistrarService : IRegistrarService
    {
        private readonly IRegistrarRepository _registrarRepository;

        public RegistrarService(IRegistrarRepository registrarRepository)
        {
            _registrarRepository = registrarRepository;
        }

        public async Task<List<Registrar>> GetAllAsync()
        {
            return await _registrarRepository.GetAllAsync();
        }

        public async Task<Registrar?> GetByIdAsync(int id)
        {
            return await _registrarRepository.GetByIdAsync(id);
        }

        public async Task AddAsync(Registrar entity)
        {
            await _registrarRepository.AddAsync(entity);
        }

        public async Task UpdateAsync(Registrar entity)
        {
            await _registrarRepository.UpdateAsync(entity);
        }

        public async Task DeleteAsync(int id)
        {
            await _registrarRepository.DeleteAsync(id);
        }
    }
}
