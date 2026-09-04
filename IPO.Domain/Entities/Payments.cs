using IPO.Domain.Common;
using IPO.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace IPO.Domain.Entities
{
    public class Payments : BaseEntity
    {
        [ForeignKey("UserId")]
        public int UserId { get; set; }
        public User User { get; set; }

        [ForeignKey("IPOId")]
        public int IPOId { get; set; }
        public IPOs IPO { get; set; }
        public decimal Amount { get; set; }
        public string? TransactionId { get; set; }
        public Payment? PaymentMethod { get; set; } 
        public string? Status { get; set; } = string.Empty;

    }
}
