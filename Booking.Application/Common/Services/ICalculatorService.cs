using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Application.Common.Services
{
    public interface ICalculatorService
    {
        decimal CalculatePriceForPeriod(DateTime start, DateTime end, decimal basePrice);
        decimal CalculateCleaningFee(int numberOfGuests);
        decimal CalculateAmenitiesUpcharge(bool hasExtraAmenities);
    }
}
