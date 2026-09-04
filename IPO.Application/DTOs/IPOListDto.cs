using System;
using System.Collections.Generic;
using System.Text;

namespace IPO.Application.DTOs
{
    public class IPOListDto
    {
        public int Id { get; set; }
        public string CompanyName { get; set; } = string.Empty;
        public string Symbol { get; set; } = string.Empty;
        public string GMP { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public DateTime OpenDate { get; set; }
        public DateTime CloseDate { get; set; }
        public DateTime? ListingDate { get; set; }
        public decimal PriceFrom { get; set; }
        public decimal PriceTo { get; set; }
        public int LotSize { get; set; }
        public decimal IssueSize { get; set; }  
        public string? ImageUrl { get; set; }
    }
}