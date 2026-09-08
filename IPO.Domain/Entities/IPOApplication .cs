using IPO.Domain.Common;
using IPO.Domain.Enums;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace IPO.Domain.Entities
{
    public class IPOApplication : BaseEntity
    {
        public string? ApplicationNumber { get; set; }
        public string Category { get; set; } = string.Empty;
        public int Lots { get; set; }
        public decimal BidPrice { get; set; }
        public decimal Amount { get; set; }
        public AllotmentStatus? AllotmentStatus { get; set; } 
        public int AppliedShares { get; set; }
        public int AllottedLots { get; set; }
        public decimal AllottedAmount { get; set; }
        public DateTime? AllotmentDate { get; set; }
        [ForeignKey("PaymentId")]
        public int PaymentId { get; set; } 
        public Payment Payment { get; set; }

        [ForeignKey("IPOId")]
        public int IPOId { get; set; }
        public IPO IPOs { get; set; }

        [ForeignKey("UserId")]
        public int UserId { get; set; }
        public User User { get; set; }
    }
}