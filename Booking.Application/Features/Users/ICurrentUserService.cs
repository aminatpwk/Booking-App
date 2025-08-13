using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Application.Features.Users
{
    public interface ICurrentUserService
    {
        Guid UserId { get; }
        string Email { get; }
    }
}
