using IPO.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace IPO.Domain.Entities
{
    public class CompanyFinancial : BaseEntity
    {
        public int FinancialYear { get; set; }
        public decimal? Revenue { get; set; }
        public decimal? Profit { get; set; }
        public decimal? NetWorth { get; set; }
        public decimal? EPS { get; set; }
        public decimal? Debt { get; set; }
        public decimal? Assets { get; set; }

        [ForeignKey("CompanyId")]
        public int CompanyId { get; set; }
        public Company Company { get; set; }
    }
}
