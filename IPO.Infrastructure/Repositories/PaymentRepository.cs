using IPO.Application.Interfaces;
using IPO.Domain.Entities;
using IPO.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace IPO.Infrastructure.Repositories
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly ApplicationDbContext _context;

        public PaymentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Payments>> GetAllAsync()
        {
            return await _context.Payments
                .ToListAsync();
        }

        public async Task<Payments?> GetByIdAsync(int id)
        {
            return await _context.Payments
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<Payments>> GetUpcomingAsync()
        {
            return await _context.Payments
                .ToListAsync();
        }

        public async Task<List<Payments>> GetOpenAsync()
        {
            return await _context.Payments
                .ToListAsync();
        }

        public async Task<List<Payments>> GetClosedAsync()
        {
            return await _context.Payments  
                .ToListAsync();
        }

        public async Task AddAsync(Payments payment)
        {
            await _context.Payments.AddAsync(payment);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Payments payment)
        {
            _context.Payments.Update(payment);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var payment = await _context.Payments.FindAsync(id);

            if (payment is null)
                return;

            _context.Payments.Remove(payment);

            await _context.SaveChangesAsync();
        }

    }
}
