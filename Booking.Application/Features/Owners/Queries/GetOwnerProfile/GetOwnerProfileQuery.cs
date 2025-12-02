using Booking.Application.Common.DTOs;
using MediatR;

namespace Booking.Application.Features.Owners.Queries.GetOwnerProfile
{
    public class GetOwnerProfileQuery : IRequest<OwnerDto?>
    {
    }
}
