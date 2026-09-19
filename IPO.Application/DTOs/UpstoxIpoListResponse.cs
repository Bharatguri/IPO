using System;
using System.Collections.Generic;
using System.Text;

namespace IPO.Application.DTOs
{
    public class UpstoxIpoListResponse
    {
        public string Status { get; set; } = string.Empty;

        public List<UpstoxIpoDto> Data { get; set; } = new();
    }

    
}
