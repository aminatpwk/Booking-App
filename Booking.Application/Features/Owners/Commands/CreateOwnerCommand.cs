using Booking.Domain.Owners;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Booking.Application.Features.Owners.Commands
{
    public class CreateOwnerCommand : IRequest<Guid>
    {
        public OwnerDto OwnerDto { get; set; }
    }
}
