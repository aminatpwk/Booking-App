using MediatR;

namespace Booking.Application.Common.Events.Bookings
{
    public class BookingCancelledEvent : BaseEvent, INotification
    {
        public Guid BookingId { get; }
        public Guid ApartmentId { get; }
        public string ApartmentName { get; }
        public Guid OwnerId { get; }

        public BookingCancelledEvent(Guid bookingId, Guid apartmentId, string apartmentName)
        {
            BookingId = bookingId;
            ApartmentId = apartmentId;
            ApartmentName = apartmentName;
        }
    }
}
