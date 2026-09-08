using IPO.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace IPO.Domain.Entities
{
    public class Watchlist : BaseEntity
    {


        [ForeignKey("IPOId")]
        public int IPOId { get; set; }
        public IPO IPOs { get; set; }

        [ForeignKey("UserId")]
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
