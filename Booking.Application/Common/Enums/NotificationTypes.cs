using Booking.Application.Common.Events.Bookings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Application.Common.Enums
{
    public enum NotificationTypes
    {
        BookingCreated = 1,
        BookingConfirmed,
        BookingCancelled
    }
}
