using Booking.Application.Common.DTOs;
using MediatR;

namespace Booking.Application.Features.Owners.Commands
{
    public class CreateOwnerCommand : IRequest<Guid>
    {
        public OwnerDto OwnerDto { get; set; }
    }
}
