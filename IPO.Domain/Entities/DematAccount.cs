using IPO.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace IPO.Domain.Entities
{
    public class DematAccount
    {
        public string? ApplicantName { get; set; }
        public string? PanNumber { get; set; } 
        public AccountType? AccountType { get; set; }
        public string? DPId { get; set; } 
        public string? BeneficiaryId { get; set; }
        public string? UpiId   { get; set; }
    }
}
