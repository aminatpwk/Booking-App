using Booking.Application.Common.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Infrastructure.Services
{
    public class CalculatorService : ICalculatorService
    {
        public decimal CalculatePriceForPeriod(DateTime start, DateTime end, decimal basePrice)
        {
            TimeSpan duration = end - start;
            int numberOfNights = (int)Math.Ceiling(duration.TotalDays);
            return numberOfNights * basePrice;
        }

        public decimal CalculateCleaningFee(int numberOfGuests)
        {
            return numberOfGuests > 2 ? 10 : 5;
        }

        public decimal CalculateAmenitiesUpcharge(bool hasExtraAmenities)
        {
            return hasExtraAmenities ? 15 : 0;
        }
    }
}
