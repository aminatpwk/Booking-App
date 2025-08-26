using Booking.Application.Common.DTOs.BookingDTOs;
using MediatR;

namespace Booking.Application.Features.Bookings.Queries.GetAllBookingsForOwner
{
    public class GetAllBookingsForOwnerQuery : IRequest<List<BookingDto>>
    {

    }
}
