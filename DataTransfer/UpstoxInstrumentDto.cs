using System;
using System.Collections.Generic;
using System.Text;

namespace IPO.Application.DTOs
{
    public class UpstoxInstrumentDto
    {
        public string InstrumentKey { get; set; } = string.Empty;
        public string TradingSymbol { get; set; } = string.Empty;
        public string Exchange { get; set; } = string.Empty;
        public string Segment { get; set; } = string.Empty;
    }
}
