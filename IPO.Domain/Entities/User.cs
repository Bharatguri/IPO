using IPO.Domain.Common;
using IPO.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace IPO.Domain.Entities
{
    public class User : AuditableEntity
    {
        public string? Name { get; set; }
        public string? Email { get; set; } 
        public string? PasswordHash { get; set; }
        public bool? IsActive { get; set; } 
    }
}
