using MediatR;

namespace Booking.Application.Common.Events.Bookings
{
    public class BookingCancelledEvent : BaseEvent, INotification
    {
        public Guid BookingId { get; }
        public Guid ApartmentId { get; }
        public string ApartmentName { get; }
        public Guid OwnerId { get; }
        public DateTime CheckIn { get; }
        public DateTime CheckOut { get; }

        public BookingCancelledEvent(Guid bookingId, Guid apartmentId, string apartmentName, Guid ownerId, DateTime checkIn, DateTime checkOut)
        {
            BookingId = bookingId;
            ApartmentId = apartmentId;
            ApartmentName = apartmentName;
            OwnerId = ownerId;
            CheckIn = checkIn;
            CheckOut = checkOut;
        }
    }
}
