using IPO.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace IPO.Application.Interfaces.Services
{
    public interface IIPOService
    {
        Task<List<IPOListDto>> GetAllAsync();
        Task<IPOListDto?> GetByIdAsync(int id);
        Task<List<IPOListDto>> GetUpcomingAsync();
        Task<List<IPOListDto>> GetOpenAsync();
        Task<List<IPOListDto>> GetClosedAsync();
        Task<IPOListDto> CreateAsync(IPOListDto request);
        Task<bool> UpdateAsync(int id, IPOListDto request);
        Task<bool> DeleteAsync(int id);
    }


}
