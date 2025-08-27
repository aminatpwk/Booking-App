using Booking.Application.Common.DTOs.BookingDTOs;
using Booking.Application.Common.Enums;

namespace Booking.Application.Common.DTOs
{
    public class NotificationDto
    {
        public NotificationTypes Type { get; set; }
        public string Message { get; set; }
        public DateTime CreatedAt { get; set; }
        public BookingNotificationPayload Payload { get; set; }
    }
}
