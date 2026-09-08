using IPO.Application.DTOs;
using IPO.Application.Interfaces;
using IPO.Application.Interfaces.Services;
using IPO.Domain.Entities;

namespace IPO.Infrastructure.Services
{
    public class IPOService : IIPOService
    {
        private readonly IIPORepository _repository;

        public IPOService(IIPORepository repository)
        {
            _repository = repository;
        }

        public async Task<List<IPOListDto>> GetAllAsync()
        {
            var ipos = await _repository.GetAllAsync();

            return ipos.Select(MapToListDto).ToList();
        }

        public async Task<IPOListDto?> GetByIdAsync(int id)
        {
            var ipo = await _repository.GetByIdAsync(id);

            if (ipo is null)
                return null;

            return MapToDetailDto(ipo);
        }

        public async Task<List<IPOListDto>> GetUpcomingAsync()
        {
            var ipos = await _repository.GetUpcomingAsync();

            return ipos.Select(MapToListDto).ToList();
        }

        public async Task<List<IPOListDto>> GetOpenAsync()
        {
            var ipos = await _repository.GetOpenAsync();

            return ipos.Select(MapToListDto).ToList();
        }

        public async Task<List<IPOListDto>> GetClosedAsync()
        {
            var ipos = await _repository.GetClosedAsync();

            return ipos.Select(MapToListDto).ToList();
        }

        public async Task<IPOListDto> CreateAsync(IPOListDto request)
        {
            var ipo = new Domain.Entities.IPO
            {
                CompanyName = request.CompanyName,
                Symbol = request.Symbol,
                GMP = request.GMP,
                OpenDate = request.OpenDate,
                CloseDate = request.CloseDate,
                ListingDate = request.ListingDate,
                PriceFrom = request.PriceFrom,
                PriceTo = request.PriceTo,
                LotSize = request.LotSize,
                IssueSize = request.IssueSize,
                ImageUrl = request.ImageUrl
            };

            await _repository.AddAsync(ipo);

            return MapToDetailDto(ipo);
        }

        public async Task<bool> UpdateAsync(int id, IPOListDto request)
        {
            var ipo = await _repository.GetByIdAsync(id);

            if (ipo is null)
                return false;

            ipo.CompanyName = request.CompanyName;
            ipo.Symbol = request.Symbol;
            ipo.GMP = request.GMP;
            ipo.OpenDate = request.OpenDate;
            ipo.CloseDate = request.CloseDate;
            ipo.ListingDate = request.ListingDate;
            ipo.PriceFrom = request.PriceFrom;
            ipo.PriceTo = request.PriceTo;
            ipo.LotSize = request.LotSize;
            ipo.IssueSize = request.IssueSize;
            ipo.ImageUrl = request.ImageUrl;

            await _repository.UpdateAsync(ipo);

            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var ipo = await _repository.GetByIdAsync(id);

            if (ipo is null)
                return false;

            await _repository.DeleteAsync(id);

            return true;
        }

        private static IPOListDto MapToListDto(Domain.Entities.IPO ipo)
        {
            return new IPOListDto
            {
                Id = ipo.Id,
                CompanyName = ipo.CompanyName,
                Symbol = ipo.Symbol,
                GMP = ipo.GMP,
                Type = ipo.Type.ToString(),
                Status = ipo.Status.ToString(),
                OpenDate = ipo.OpenDate,
                CloseDate = ipo.CloseDate,
                ListingDate = ipo.ListingDate,
                PriceFrom = ipo.PriceFrom,
                PriceTo = ipo.PriceTo,
                LotSize = ipo.LotSize,
                IssueSize = ipo.IssueSize,
                ImageUrl = ipo.ImageUrl
            };
        }

        private static IPOListDto MapToDetailDto(Domain.Entities.IPO ipo)
        {
            return new IPOListDto
            {
                Id = ipo.Id,
                CompanyName = ipo.CompanyName,
                Symbol = ipo.Symbol,
                GMP = ipo.GMP,
                Type = ipo.Type.ToString(),
                Status = ipo.Status.ToString(),
                OpenDate = ipo.OpenDate,
                CloseDate = ipo.CloseDate,
                ListingDate = ipo.ListingDate,
                PriceFrom = ipo.PriceFrom,
                PriceTo = ipo.PriceTo,
                LotSize = ipo.LotSize,
                IssueSize = ipo.IssueSize,
                ImageUrl = ipo.ImageUrl
            };
        }
    }
}
