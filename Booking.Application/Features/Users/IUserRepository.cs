using Booking.Application.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Booking.Domain.Users;
namespace Booking.Application.Features.Users
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<bool> IsEmailUnique(string email, CancellationToken cancellationToken);
    }
}
