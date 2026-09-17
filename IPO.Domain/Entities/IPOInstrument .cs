using IPO.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace IPO.Domain.Entities
{
    public class IPOInstrument : AuditableEntity
    {
        public int IPOId { get; set; }
        public string Symbol { get; set; } = string.Empty;
        public string InstrumentKey { get; set; } = string.Empty;
        public string Exchange { get; set; } 
        public IPO IPO { get; set; } = null!;
    }
}