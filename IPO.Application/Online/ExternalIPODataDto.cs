using System;
using System.Collections.Generic;
using System.Text;

namespace IPO.Application.Online
{
    public class ExternalIPODataDto
    {
        public string CompanyName { get; set; } = string.Empty;
        public string Symbol { get; set; } = string.Empty;
        public string? OpenDate { get; set; }
        public string? CloseDate { get; set; }
        public string? ListingDate { get; set; }
        public decimal MinPrice { get; set; }
        public decimal MaxPrice { get; set; }
        public int LotSize { get; set; }
        public decimal IssueSize { get; set; }
        public string? Type { get; set; }
        public string? Status { get; set; }
        public string? ImageUrl { get; set; }
    }
}
