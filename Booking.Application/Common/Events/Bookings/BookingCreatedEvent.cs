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

        public BookingCreatedEvent(Guid bookingId, Guid ownerId, string apartmentName)
        {
            BookingId = bookingId;
            OwnerId = ownerId;
            ApartmentName = apartmentName;
        }
    }
}
