using IPO.Domain.Common;
using IPO.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace IPO.Domain.Entities
{
    public class User : AuditableEntity
    {
        public Role Role { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; } 
        public string? Password { get; set; }
        public bool? IsActive { get; set; } 
    }
}
