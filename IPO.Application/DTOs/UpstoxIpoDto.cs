using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace IPO.Application.DTOs
{
    public class UpstoxIpoDto
    {
        [JsonPropertyName("id")]
        public string Id { get; set; } = string.Empty;

        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;

        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("status")]
        public string Status { get; set; } = string.Empty;

        [JsonPropertyName("issue_type")]
        public string IssueType { get; set; } = string.Empty;

        [JsonPropertyName("issue_size")]
        public decimal IssueSize { get; set; }

        [JsonPropertyName("minimum_price")]
        public decimal MinimumPrice { get; set; }

        [JsonPropertyName("maximum_price")]
        public decimal MaximumPrice { get; set; }

        [JsonPropertyName("bidding_start_date")]
        public string BiddingStartDate { get; set; } = string.Empty;

        [JsonPropertyName("bidding_end_date")]
        public string BiddingEndDate { get; set; } = string.Empty;
    }
}
