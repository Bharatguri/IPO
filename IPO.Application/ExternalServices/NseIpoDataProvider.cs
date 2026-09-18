using IPO.Application.Online;
using System;
using System.Collections.Generic;
using System.Text;

namespace IPO.Application.ExternalServices
{
    public class NseIpoDataProvider : IIpoDataProvider
    {
        private readonly HttpClient _httpClient;

        private const string NseIpoPage =
            "https://www.nseindia.com/market-data/all-upcoming-issues-ipo";

        public NseIpoDataProvider(HttpClient httpClient)
        {
            _httpClient = httpClient;

            _httpClient.DefaultRequestHeaders.TryAddWithoutValidation(
                "User-Agent",
                "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 Chrome/140 Safari/537.36");

            _httpClient.DefaultRequestHeaders.TryAddWithoutValidation(
                "Accept",
                "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8");

            _httpClient.DefaultRequestHeaders.TryAddWithoutValidation(
                "Accept-Language",
                "en-US,en;q=0.9");
        }

        public async Task<List<Domain.Entities.IPO>> GetIPOsAsync(
            CancellationToken cancellationToken = default)
        {
            var response = await _httpClient.GetAsync(
                NseIpoPage,
                cancellationToken);

            response.EnsureSuccessStatusCode();

            var html = await response.Content.ReadAsStringAsync(
                cancellationToken);

            // Temporary diagnostic.
            // We will replace this with the actual NSE data parser.
            if (string.IsNullOrWhiteSpace(html))
            {
                throw new InvalidOperationException(
                    "NSE returned an empty response.");
            }

            return new List<Domain.Entities.IPO>();
        }
    }
}
