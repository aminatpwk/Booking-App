using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Booking.Domain.Bookings;

namespace Booking.Domain.Reviews
{
    public class Review
    {
        [Key]
        public Guid Id { get; set; }

        [ForeignKey(nameof(Apartment))]
        public Guid ApartmentId { get; private set; }
        public Apartment Apartment { get; set; }
        public int Rating { get; private set; } 
        public string? Comment { get; private set; }
        public DateTime CreatedOnUtc { get; private set; }

    }
}
