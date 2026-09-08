using IPO.Application.Interfaces;
using IPO.Application.Interfaces.Services;
using IPO.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace IPO.Application.Services
{

    public class PaymentService : IPaymentService
    {
        private readonly IPaymentRepository _paymentRepository;

        public PaymentService(IPaymentRepository paymentRepository)
        {
            _paymentRepository = paymentRepository;
        }

        public async Task<List<Payment>> GetAllAsync()
        {
            return await _paymentRepository.GetAllAsync();
        }

        public async Task<Payment?> GetByIdAsync(int id)
        {
            return await _paymentRepository.GetByIdAsync(id);
        }

        public async Task AddAsync(Payment entity)
        {
            await _paymentRepository.AddAsync(entity);
        }

        public async Task UpdateAsync(Payment entity)
        {
            await _paymentRepository.UpdateAsync(entity);
        }

        public async Task DeleteAsync(int id)
        {
            await _paymentRepository.DeleteAsync(id);
        }
    }
}
