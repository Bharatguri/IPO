using System;
using System.Collections.Generic;
using System.Text;

namespace IPO.Domain.Enums
{
    public enum PaymentStatus
    {
        Pending = 1,
        Completed = 2,
        Failed = 3,
        Refunded = 4,
        Cancelled = 5,
        Blocked = 6,
        UnBlocked = 7,
        Debited = 8
    }
}
