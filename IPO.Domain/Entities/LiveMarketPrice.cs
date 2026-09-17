using IPO.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace IPO.Domain.Entities
{
    public class LiveMarketPrice : AuditableEntity
    {
        public int IPOInstrumentId { get; set; }
        public string InstrumentKey { get; set; } = string.Empty;
        public decimal LTP { get; set; }
        public decimal PreviousClose { get; set; }
        public long? LastTradeTime { get; set; }
        public DateTime UpdatedAt { get; set; }
        public IPOInstrument IPOInstrument { get; set; } = null!;
    }
}
