using IPO.Application.DTOs;
using IPO.Domain.Entities;
using IPO.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace IPO.API.Services
{
    public class UpstoxHistoricalService
    {
        private readonly UpstoxApiClient _client;
        private readonly ApplicationDbContext _db;

        public UpstoxHistoricalService(
            UpstoxApiClient client,
            ApplicationDbContext db)
        {
            _client = client;
            _db = db;
        }

        // ================================
        // GET FROM UPSTOX
        // ================================
        public async Task<List<UpstoxCandleDto>> GetOneMinuteCandlesAsync(
            string instrumentKey,
            DateTime fromDate,
            DateTime toDate,
            CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(instrumentKey))
                throw new ArgumentException("Instrument key is required.");

            if (fromDate > toDate)
                throw new ArgumentException(
                    "fromDate cannot be greater than toDate.");

            var url =
                $"/v3/historical-candle/" +
                $"{instrumentKey}/minutes/1/" +
                $"{toDate:yyyy-MM-dd}/" +
                $"{fromDate:yyyy-MM-dd}";

            using var document =
                await _client.GetAsync(
                    url,
                    cancellationToken);

            var candles =
                document.RootElement
                    .GetProperty("data")
                    .GetProperty("candles");

            var result = new List<UpstoxCandleDto>();

            foreach (var candle in candles.EnumerateArray())
            {
                if (candle.GetArrayLength() < 6)
                    continue;

                result.Add(new UpstoxCandleDto
                {
                    CandleTime = candle[0].GetDateTime(),
                    Open = candle[1].GetDecimal(),
                    High = candle[2].GetDecimal(),
                    Low = candle[3].GetDecimal(),
                    Close = candle[4].GetDecimal(),
                    Volume = candle[5].GetInt64()
                });
            }

            return result;
        }


        // ================================
        // SAVE CANDLES TO DATABASE
        // ================================
        public async Task<int> SaveOneMinuteCandlesAsync(
            int ipoInstrumentId,
            string instrumentKey,
            List<UpstoxCandleDto> candles,
            CancellationToken cancellationToken = default)
        {
            if (candles == null || candles.Count == 0)
                return 0;

            var candleTimes = candles
                .Select(x => x.CandleTime)
                .ToList();

            // Existing candles check
            var existingTimes = await _db.OneMinuteCandles
                .Where(x =>
                    x.IPOInstrumentId == ipoInstrumentId &&
                    candleTimes.Contains(x.CandleTime))
                .Select(x => x.CandleTime)
                .ToListAsync(cancellationToken);

            var existingSet = existingTimes.ToHashSet();

            var newCandles = candles
                .Where(x => !existingSet.Contains(x.CandleTime))
                .Select(x => new OneMinuteCandle
                {
                    IPOInstrumentId = ipoInstrumentId,

                    InstrumentKey = instrumentKey,

                    CandleTime = x.CandleTime,

                    Open = x.Open,

                    High = x.High,

                    Low = x.Low,

                    Close = x.Close,

                    Volume = x.Volume
                })
                .ToList();

            if (newCandles.Count == 0)
                return 0;

            await _db.OneMinuteCandles.AddRangeAsync(
                newCandles,
                cancellationToken);

            await _db.SaveChangesAsync(
                cancellationToken);

            return newCandles.Count;
        }
    }

}
