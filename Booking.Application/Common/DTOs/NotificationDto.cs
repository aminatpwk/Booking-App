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
        public Guid BookingId { get; set; }
        public Guid ApartmentId { get; set; }
        public string ApartmentName { get; set; }
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
        public Guid OwnerId { get; set; }
        public BookingStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Message { get; set; }
    }
}
