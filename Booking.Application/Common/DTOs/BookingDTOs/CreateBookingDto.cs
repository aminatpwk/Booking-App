using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Application.Common.DTOs.BookingDTOs
{
    //this dto is used for commands
    public class CreateBookingDto
    {
        public Guid ApartmentId { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
    }
}
