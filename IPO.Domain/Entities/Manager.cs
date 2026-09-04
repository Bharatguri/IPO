using IPO.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace IPO.Domain.Entities
{
    public class Manager : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string? WebsiteUrl { get; set; }

        [ForeignKey("IPOId")]
        public int IPOId { get; set; }
        public IPOs IPO { get; set; }
    }
}
