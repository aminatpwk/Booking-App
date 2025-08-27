using Booking.Application.Common.DTOs.BookingDTOs;

namespace Booking.Application.Common.Services
{
    public interface IPdfGeneratorService
    {
        byte[] GenerateBookingDetailsPdf(BookingDto bookingDto);
    }
}
