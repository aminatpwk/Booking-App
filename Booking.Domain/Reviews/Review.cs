using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Booking.Domain.Users;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Domain.Reviews
{
    public class Review
    {
        [Key]
        public Guid Id { get; set; }

        [ForeignKey(nameof(Apartment))]
        public Guid ApartmentId { get; private set; }
        public Apartment Apartment { get; set; }

        [ForeignKey(nameof(User))]
        public Guid UserId { get; private set; }
        public User User { get; set; }
        public decimal Rating { get; private set; } 
        public string? Comment { get; private set; }
        public DateTime CreatedOnUtc { get; private set; }

        public Review()
        {

        }

        public static Review Create(Guid apartmentId, Guid userId, decimal rating, string comment)
        {
            return new Review
            {
                Id = Guid.NewGuid(),
                ApartmentId = apartmentId,
                UserId = userId,
                Rating = rating,
                Comment = comment,
                CreatedOnUtc = DateTime.UtcNow
            };
        }

    }
}
