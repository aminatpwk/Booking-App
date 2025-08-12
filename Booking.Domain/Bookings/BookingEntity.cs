using Booking.Domain.Reviews;
using Booking.Domain.Users;
using Booking.Domain.Apartments;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Domain.Bookings
{
    public class BookingEntity
    {
        [Key]
        public Guid Id { get; set; }

        [ForeignKey(nameof(Apartment))]
        public Guid ApartmentId { get; private set; }
        public Apartment Apartment { get; private set; }

        [ForeignKey(nameof(User))]
        public Guid UserId { get; private set; }
        public User User { get; private set; }
        public DateTime Start { get; private set; }
        public DateTime End { get; private set; }
        public decimal PriceForPeriod { get; private set; }
        public decimal CleaningFee { get; private set; }
        public decimal AmenitiesUpCharge { get; private set; }
        public decimal TotalPrice { get; private set; }
        public BookingStatus Status { get; private set; }
        public DateTime CreatedOnUtc { get; private set; }
        public DateTime? ConfirmedOnUtc { get; private set; }
        public DateTime? RejectedOnUtc { get; private set; }
        public DateTime? CompletedOnUtc { get; private set; }
        public DateTime? CancelledOnUtc { get; private set; }

        public static BookingEntity Create(Guid apartmentId, Guid userId, DateTime start, DateTime end, decimal priceForPeriod, decimal cleaningFee, decimal amenitiesUpCharge)
        {
            return new BookingEntity
            {
                Id = Guid.NewGuid(),
                ApartmentId = apartmentId,
                UserId = userId,
                Start = start,
                End = end,
                PriceForPeriod = priceForPeriod,
                CleaningFee = cleaningFee,
                AmenitiesUpCharge = amenitiesUpCharge,
                TotalPrice = priceForPeriod + cleaningFee + amenitiesUpCharge,
                Status = BookingStatus.PendingApproval,
                CreatedOnUtc = DateTime.UtcNow
            };
        }
    }

}
