using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Application.Common.DTOs
{
    public class ReviewDto
    {
        public Guid ApartmentId { get; set; }
        public decimal Rating { get; set; }
        public string Comment { get; set; }
    }
}
