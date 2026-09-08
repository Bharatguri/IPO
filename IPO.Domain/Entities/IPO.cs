using IPO.Domain.Common;
using IPO.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace IPO.Domain.Entities
{
    public class IPO : AuditableEntity
    {
        public string CompanyName { get; set; } = string.Empty;
        public string Symbol { get; set; } = string.Empty;
        public string GMP { get; set; } = string.Empty;
        public IPOType Type { get; set; }
        public IPOStatus Status { get; set; }
        public DateTime OpenDate { get; set; }
        public DateTime CloseDate { get; set; }
        public DateTime? ListingDate { get; set; }
        public decimal PriceMin  { get; set; }
        public decimal PriceMax { get; set; }
        public int LotSize { get; set; }
        public decimal IssueSize { get; set; }
        public string? ImageUrl { get; set; }
       
    }
}
