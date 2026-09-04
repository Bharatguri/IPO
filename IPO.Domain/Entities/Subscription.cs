using IPO.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace IPO.Domain.Entities
{
    public class Subscription : AuditableEntity
    {

        public int Id { get; set; }

        [ForeignKey("IPOId")]
        public int IPOId { get; set; }
        public IPOs IPOs { get; set; }
        public string Category { get; set; } = string.Empty;
        public decimal SubscriptionMultiple { get; set; }
        public decimal SharesOffered { get; set; }
        public decimal SharesApplied { get; set; }
        public decimal Amount { get; set; }
        public int Day { get; set; }
        public DateTime SubscriptionDate { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
    
    }
}
