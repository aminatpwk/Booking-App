using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Booking.Domain.Users;
using MediatR;

namespace Booking.Application.Features.Users.Commands.CreateUser
{
    public class CreateUserCommand : IRequest<Guid>
    {
        public UserDto UserDto { get; set; }
    }
}
