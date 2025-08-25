using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Application.Common.Events.Bookings
{
    public class BookingConfirmedEvent : BaseEvent, INotification
    {
        public Guid BookingId { get; }
        public Guid ApartmentId { get; }
        public Guid OwnerId { get; }
        public string ApartmentName { get; }
        public DateTime CheckIn { get; }
        public DateTime CheckOut { get; }

        public BookingConfirmedEvent(Guid bookingId, Guid apartmentId, Guid ownerId, string apartmentName, DateTime checkIn, DateTime checkOut)
        {
            BookingId = bookingId;
            ApartmentId = apartmentId;
            OwnerId = ownerId;
            ApartmentName = apartmentName;
            CheckIn = checkIn;
            CheckOut = checkOut;
        }
    }
}
