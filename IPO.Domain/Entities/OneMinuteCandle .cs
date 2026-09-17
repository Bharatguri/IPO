using IPO.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace IPO.Domain.Entities
{
    public class OneMinuteCandle : AuditableEntity
    {
        public int IPOInstrumentId { get; set; }
        public string InstrumentKey { get; set; } = string.Empty;
        public DateTime CandleTime { get; set; }
        public decimal Open { get; set; }
        public decimal High { get; set; }
        public decimal Low { get; set; }
        public decimal Close { get; set; }
        public long Volume { get; set; }
        public IPOInstrument IPOInstrument { get; set; } = null!;
    }

}
