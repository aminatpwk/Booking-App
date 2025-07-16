using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Domain.Bookings
{
    public enum BookingStatus
    {
        Reserved = 1,
        Confirmed,
        Rejected,
        Cancelled,
        Completed
    }
}
