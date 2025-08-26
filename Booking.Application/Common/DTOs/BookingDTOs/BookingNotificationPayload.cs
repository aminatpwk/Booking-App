using Booking.Domain.Bookings;


namespace Booking.Application.Common.DTOs.BookingDTOs
{
    public class BookingNotificationPayload
    {
        public Guid BookingId { get; set; }
        public Guid ApartmentId { get; set; }
        public string ApartmentName { get; set; }
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
        public BookingStatus Status { get; set; }
    }
}
