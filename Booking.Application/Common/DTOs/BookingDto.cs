using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Application.Common.DTOs
{
    public class BookingDto
    {
        public Guid ApartmentId { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
    }
}
