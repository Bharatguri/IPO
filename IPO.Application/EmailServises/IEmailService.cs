using System;
using System.Collections.Generic;
using System.Text;

namespace IPO.Application.EmailServises
{
    public interface IEmailService
    {
        Task SendEmail(string email, string subject, string message);
    }
}
