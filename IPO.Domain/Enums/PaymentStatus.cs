using System;
using System.Collections.Generic;
using System.Text;

namespace IPO.Domain.Enums
{
    public enum PaymentStatus
    {
        Pending = 1,
        Blocked = 6,
        UnBlocked = 7,
        Debited = 8,
        Failed = 3
    }
}
