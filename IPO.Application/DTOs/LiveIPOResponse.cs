using System;
using System.Collections.Generic;
using System.Text;

namespace IPO.Application.DTOs
{
    public class LiveIPOResponse
    {
        public string Id { get; set; } = string.Empty;

        public string Symbol { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;

        public string Status { get; set; } = string.Empty;

        public string IssueType { get; set; } = string.Empty;

        public decimal IssueSize { get; set; }

        public decimal MinimumPrice { get; set; }

        public decimal MaximumPrice { get; set; }

        public DateTime BiddingStartDate { get; set; }

        public DateTime BiddingEndDate { get; set; }

        public int LotSize { get; set; }

        public DateTime? ListingDate { get; set; }

        public string TotalSubscription { get; set; } = string.Empty;
    }
}
