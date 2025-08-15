using Booking.Application.Common.Services;
using Booking.Application.Features.Bookings;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Infrastructure.Services
{
    public class BookingStatusUpdaterJob : IBookingStatusUpdaterJob
    {
        private readonly ILogger _logger;
        private readonly IBookingRepository _bookingRepository;
        public BookingStatusUpdaterJob(ILogger<BookingStatusUpdaterJob> logger, IBookingRepository bookingRepository)
        {
            _logger = logger;
            _bookingRepository = bookingRepository;
        }

        public async Task UpdateCompletedBookings()
        {
            try
            {
                var completedBookings = await _bookingRepository.GetExpiredConfirmedBookings();
                if (!completedBookings.Any())
                {
                    _logger.LogInformation("No completed bookings that have expired found!");
                    return;
                }

                foreach(var booking in completedBookings)
                {
                    try
                    {
                        booking.Complete();
                        await _bookingRepository.Update(booking);
                    }catch(Exception ex)
                    {
                        _logger.LogError(ex, "Failed to complete booking!");
                    }
                }

            }catch(Exception ex)
            {
                _logger.LogError(ex, "Error occurred during booking status update!");
                throw;
            }
        }


    }
}
