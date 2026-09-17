using IPO.API.Services;
using IPO.Domain.Entities;
using IPO.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IPO.API.Controllers
{
    [ApiController]
    [Route("api/upstox")]
    public class UpstoxController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        private readonly UpstoxHistoricalService _historicalService;
        private readonly UpstoxInstrumentService _instrumentService;

        public UpstoxController(
            ApplicationDbContext db,
            UpstoxHistoricalService historicalService,
            UpstoxInstrumentService instrumentService)
        {
            _db = db;
            _historicalService = historicalService;
            _instrumentService = instrumentService;
        }


        // ============================================================
        // 1. TEST - GET HISTORICAL DATA FROM UPSTOX
        // ============================================================

        [HttpGet("historical")]
        public async Task<IActionResult> GetHistorical(
            [FromQuery] string instrumentKey,
            [FromQuery] DateTime fromDate,
            [FromQuery] DateTime toDate,
            CancellationToken cancellationToken)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(instrumentKey))
                {
                    return BadRequest(new
                    {
                        message = "Instrument key is required."
                    });
                }

                if (fromDate > toDate)
                {
                    return BadRequest(new
                    {
                        message = "fromDate cannot be greater than toDate."
                    });
                }

                var candles =
                    await _historicalService.GetOneMinuteCandlesAsync(
                        instrumentKey,
                        fromDate,
                        toDate,
                        cancellationToken);

                return Ok(new
                {
                    instrumentKey,
                    fromDate,
                    toDate,
                    count = candles.Count,
                    candles
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "Error while getting historical data.",
                    error = ex.Message
                });
            }
        }


        // ============================================================
        // 2. SYNC ALL IPOs WITH UPSTOX INSTRUMENTS
        // ============================================================

        [HttpPost("sync-instruments")]
        public async Task<IActionResult> SyncInstruments(
            CancellationToken cancellationToken)
        {
            try
            {
                // Get all listed IPOs
                var ipos = await _db.IPOs
                    .Where(x =>
                        x.ListingDate != null &&
                        x.ListingDate <= DateTime.UtcNow)
                    .ToListAsync(cancellationToken);

                if (ipos.Count == 0)
                {
                    return Ok(new
                    {
                        message = "No listed IPOs found.",
                        totalIpos = 0
                    });
                }

                var result = new List<object>();

                foreach (var ipo in ipos)
                {
                    try
                    {
                        // --------------------------------------------
                        // Check symbol
                        // --------------------------------------------

                        if (string.IsNullOrWhiteSpace(ipo.Symbol))
                        {
                            result.Add(new
                            {
                                ipoId = ipo.Id,
                                status = "Skipped",
                                reason = "IPO symbol is empty."
                            });

                            continue;
                        }


                        // --------------------------------------------
                        // Search instrument on Upstox
                        // --------------------------------------------

                        var instrument =
                            await _instrumentService
                                .SearchNseEquityAsync(
                                    ipo.Symbol,
                                    cancellationToken);


                        // --------------------------------------------
                        // Instrument not found
                        // --------------------------------------------

                        if (instrument == null)
                        {
                            result.Add(new
                            {
                                ipoId = ipo.Id,
                                symbol = ipo.Symbol,
                                status = "NotFound",
                                reason =
                                    "NSE equity instrument not found on Upstox."
                            });

                            continue;
                        }


                        // --------------------------------------------
                        // Check existing mapping
                        // --------------------------------------------

                        var existing =
                            await _db.IPOInstruments
                                .FirstOrDefaultAsync(
                                    x => x.IPOId == ipo.Id,
                                    cancellationToken);


                        // --------------------------------------------
                        // Create new mapping
                        // --------------------------------------------

                        if (existing == null)
                        {
                            existing = new IPOInstrument
                            {
                                IPOId = ipo.Id,

                                Symbol = ipo.Symbol,

                                InstrumentKey =
                                    instrument.InstrumentKey,

                                Exchange =
                                    instrument.Exchange,

                                IsActive = true
                            };

                            await _db.IPOInstruments.AddAsync(
                                existing,
                                cancellationToken);
                        }
                        else
                        {
                            // ----------------------------------------
                            // Update existing mapping
                            // ----------------------------------------

                            existing.Symbol =
                                ipo.Symbol;

                            existing.InstrumentKey =
                                instrument.InstrumentKey;

                            existing.Exchange =
                                instrument.Exchange;

                            existing.IsActive = true;
                        }


                        result.Add(new
                        {
                            ipoId = ipo.Id,
                            symbol = ipo.Symbol,
                            instrumentKey =
                                instrument.InstrumentKey,
                            exchange =
                                instrument.Exchange,
                            status = "Mapped"
                        });
                    }
                    catch (Exception ex)
                    {
                        result.Add(new
                        {
                            ipoId = ipo.Id,
                            symbol = ipo.Symbol,
                            status = "Error",
                            error = ex.Message
                        });
                    }
                }


                // --------------------------------------------
                // Save all mappings
                // --------------------------------------------

                await _db.SaveChangesAsync(
                    cancellationToken);


                return Ok(new
                {
                    message =
                        "IPO instruments synchronization completed.",

                    totalIpos = ipos.Count,

                    mapped = result.Count(x =>
                        x.ToString()!.Contains("Mapped")),

                    result
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message =
                        "Error while synchronizing IPO instruments.",

                    error = ex.Message
                });
            }
        }


        // ============================================================
        // 3. IMPORT HISTORICAL DATA FOR ONE IPO
        // ============================================================

        [HttpPost("historical/import")]
        public async Task<IActionResult> ImportHistorical(
            [FromQuery] int ipoId,
            [FromQuery] DateTime fromDate,
            [FromQuery] DateTime toDate,
            CancellationToken cancellationToken)
        {
            try
            {
                // --------------------------------------------
                // Find IPO
                // --------------------------------------------

                var ipo = await _db.IPOs
                    .FirstOrDefaultAsync(
                        x => x.Id == ipoId,
                        cancellationToken);

                if (ipo == null)
                {
                    return NotFound(new
                    {
                        message = "IPO not found.",
                        ipoId
                    });
                }


                // --------------------------------------------
                // Find Upstox instrument mapping
                // --------------------------------------------

                var instrument =
                    await _db.IPOInstruments
                        .FirstOrDefaultAsync(
                            x =>
                                x.IPOId == ipoId &&
                                x.IsActive,
                            cancellationToken);


                // --------------------------------------------
                // Mapping doesn't exist
                // --------------------------------------------

                if (instrument == null)
                {
                    return BadRequest(new
                    {
                        message =
                            "Upstox instrument mapping not found.",

                        ipoId,

                        symbol = ipo.Symbol,

                        suggestion =
                            "Run POST /api/upstox/sync-instruments first."
                    });
                }


                // --------------------------------------------
                // Get historical data
                // --------------------------------------------

                var candles =
                    await _historicalService
                        .GetOneMinuteCandlesAsync(
                            instrument.InstrumentKey,
                            fromDate,
                            toDate,
                            cancellationToken);


                // --------------------------------------------
                // Save to database
                // --------------------------------------------

                var inserted =
                    await _historicalService
                        .SaveOneMinuteCandlesAsync(
                            instrument.Id,
                            instrument.InstrumentKey,
                            candles,
                            cancellationToken);


                return Ok(new
                {
                    message =
                        "Historical data imported successfully.",

                    ipoId,

                    symbol =
                        ipo.Symbol,

                    instrumentId =
                        instrument.Id,

                    instrumentKey =
                        instrument.InstrumentKey,

                    fromDate,

                    toDate,

                    received =
                        candles.Count,

                    inserted
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message =
                        "Error while importing historical data.",

                    error = ex.Message
                });
            }
        }


        // ============================================================
        // 4. GET IPO MARKET DATA
        // ============================================================

        [HttpGet("ipo/{ipoId}/market-data")]
        public async Task<IActionResult> GetIPOMarketData(
            int ipoId,
            CancellationToken cancellationToken)
        {
            try
            {
                // --------------------------------------------
                // Find IPO
                // --------------------------------------------

                var ipo = await _db.IPOs
                    .FirstOrDefaultAsync(
                        x => x.Id == ipoId,
                        cancellationToken);

                if (ipo == null)
                {
                    return NotFound(new
                    {
                        message = "IPO not found.",
                        ipoId
                    });
                }


                // --------------------------------------------
                // Find instrument
                // --------------------------------------------

                var instrument =
                    await _db.IPOInstruments
                        .FirstOrDefaultAsync(
                            x =>
                                x.IPOId == ipoId && x.IsActive,
                            cancellationToken);


                if (instrument == null)
                {
                    return NotFound(new
                    {
                        message =
                            "Upstox instrument mapping not found.",

                        ipoId,

                        symbol =
                            ipo.Symbol
                    });
                }


                // --------------------------------------------
                // Latest live price
                // --------------------------------------------

                var livePrice =
                    await _db.LiveMarketPrices
                        .FirstOrDefaultAsync(
                            x =>
                                x.IPOInstrumentId ==
                                instrument.Id,
                            cancellationToken);


                // --------------------------------------------
                // Recent 1-minute candles
                // --------------------------------------------

                var candles =
                    await _db.OneMinuteCandles
                        .Where(x =>
                            x.IPOInstrumentId ==
                            instrument.Id)
                        .OrderByDescending(
                            x => x.CandleTime)
                        .Take(100)
                        .OrderBy(
                            x => x.CandleTime)
                        .ToListAsync(
                            cancellationToken);


                return Ok(new
                {
                    ipo = new
                    {
                        id = ipo.Id,
                        companyName = ipo.CompanyName,
                        symbol = ipo.Symbol,
                        listingDate = ipo.ListingDate
                    },

                    instrument = new
                    {
                        id = instrument.Id,
                        instrumentKey =
                            instrument.InstrumentKey,
                        exchange =
                            instrument.Exchange
                    },

                    livePrice,

                    candles
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message =
                        "Error while getting IPO market data.",

                    error = ex.Message
                });
            }
        }
    }
}
