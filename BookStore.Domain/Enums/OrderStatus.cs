using System;
using System.Collections.Generic;
using System.Text;

namespace BookStore.Domain.Enums
{
    public enum OrderStatus
    {
        PENDING = 1,
        PROCESSING = 2,
        SHIPPED = 3,
        COMPLETED = 4,
        DISPUTED = 5,
        REFUNDED = 6,
        CANCELED = 7
    }
}
