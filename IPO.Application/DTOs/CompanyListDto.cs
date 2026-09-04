using System;
using System.Collections.Generic;
using System.Text;

namespace IPO.Application.DTOs
{
    public class CompanyListDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? LogoUrl { get; set; }
        public string? About { get; set; }
        public string? WebsiteUrl { get; set; }
        public string? Address { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
