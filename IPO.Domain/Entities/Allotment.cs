using IPO.Domain.Common;
using IPO.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace IPO.Domain.Entities
{
    public class Allotment : AuditableEntity
    {
        public DateTime AllotmentDate { get; set; }
        public AllotmentStatus Status { get; set; }

        [ForeignKey("IPOId")]
        public int IPOId { get; set; }
        public IPOs IPO { get; set; }
    }
}
