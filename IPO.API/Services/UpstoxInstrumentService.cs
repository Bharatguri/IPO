using IPO.Application.DTOs;

namespace IPO.API.Services
{
    public class UpstoxInstrumentService
    {
        private readonly UpstoxApiClient _client;

        public UpstoxInstrumentService(UpstoxApiClient client)
        {
            _client = client;
        }

        public async Task<UpstoxInstrumentDto?> SearchNseEquityAsync(
            string symbol,
            CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(symbol))
                return null;

            var query = Uri.EscapeDataString(symbol.Trim());

            var url =
                $"/v2/instruments/search" +
                $"?query={query}" +
                $"&exchanges=NSE" +
                $"&segments=EQ" +
                $"&page_number=1" +
                $"&records=30";

            using var document =
                await _client.GetAsync(
                    url,
                    cancellationToken);

            if (!document.RootElement.TryGetProperty(
                    "data",
                    out var data))
            {
                return null;
            }

            foreach (var item in data.EnumerateArray())
            {
                var instrumentType =
                    item.TryGetProperty(
                        "instrument_type",
                        out var type)
                        ? type.GetString()
                        : null;

                var segment =
                    item.TryGetProperty(
                        "segment",
                        out var segmentProperty)
                        ? segmentProperty.GetString()
                        : null;

                var tradingSymbol =
                    item.TryGetProperty(
                        "trading_symbol",
                        out var tradingSymbolProperty)
                        ? tradingSymbolProperty.GetString()
                        : null;

                // We only want NSE Equity
                if (segment != "NSE_EQ")
                    continue;

                if (instrumentType != "EQ" &&
                    instrumentType != "A" &&
                    instrumentType != "X")
                    continue;

                if (!string.Equals(
                        tradingSymbol,
                        symbol.Trim(),
                        StringComparison.OrdinalIgnoreCase))
                {
                    continue;
                }

                var instrumentKey =
                    item.GetProperty("instrument_key")
                        .GetString();

                if (string.IsNullOrWhiteSpace(instrumentKey))
                    continue;

                return new UpstoxInstrumentDto
                {
                    InstrumentKey = instrumentKey,
                    TradingSymbol = tradingSymbol ?? symbol,
                    Exchange = "NSE",
                    Segment = segment
                };
            }

            return null;
        }
    }
}
