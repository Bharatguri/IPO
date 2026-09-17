using IPO.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace IPO.Domain.Entities
{
    public class IPODocument : BaseEntity
    {
        public string DocumentType { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string FileUrl { get; set; } = string.Empty;

        [ForeignKey("IPOId")]
        public int IPOId { get; set; }
        public IPO IPOs { get; set; }
    }
}
