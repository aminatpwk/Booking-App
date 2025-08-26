using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Application.Common.Events.Bookings
{
    public class BookingCreatedEvent : BaseEvent, INotification
    {
        public Guid BookingId { get; }
        public Guid OwnerId { get; }
        public string ApartmentName { get; }
        public Guid ApartmentId { get; }
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }

        public BookingCreatedEvent(Guid bookingId, Guid apartmentId, Guid ownerId, string apartmentName, DateTime checkIn, DateTime checkOut)
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
