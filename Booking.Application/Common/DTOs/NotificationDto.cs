using Booking.Application.Common.Enums;
using Booking.Domain.Bookings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Application.Common.DTOs
{
    public class NotificationDto
    {
        public NotificationTypes Type { get; set; }
        public string Message { get; set; }
        public DateTime CreatedAt { get; set; }
        public object? Payload { get; set; }
    }
}
