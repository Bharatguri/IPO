//using IPO.Domain.Common;
//using System;
//using System.Collections.Generic;
//using System.ComponentModel.DataAnnotations.Schema;
//using System.Text;

//namespace IPO.Domain.Entities
//{
//    public class GMP : AuditableEntity
//    {
//        public decimal GMPPrice { get; set; }
//        public decimal GMPPercent { get; set; }
//        public string? Remarks { get; set; }

//        [ForeignKey("IPOId")]
//        public IPO IPOId { get; set; };
//        public IPO IPO { get; set; };
//    }
//}
