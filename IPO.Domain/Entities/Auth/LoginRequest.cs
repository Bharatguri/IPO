using IPO.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace IPO.Domain.Entities.Auth
{
    public class LoginRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
