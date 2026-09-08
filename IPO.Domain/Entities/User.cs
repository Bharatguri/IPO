using IPO.Domain.Common;
using IPO.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Numerics;
using System.Text;

namespace IPO.Domain.Entities
{
    public class User : AuditableEntity
    {
        public Role Role { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; } 
        public string? PhoneNumber { get; set; }
        public string? Password { get; set; }
        public string? Address { get; set; }
        public string PANNumber { get; set; } = string.Empty;
        //public string? BankAccountNumber { get; set; }
        public string AdharNumber { get; set; } = string.Empty;
        public bool? IsActive { get; set; } 
    }
}
