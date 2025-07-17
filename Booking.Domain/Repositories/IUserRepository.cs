using Booking.Domain.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Domain.Repositories
{
    public interface IUserRepository
    {
        Task<User> CreateAsync(User user);
        Task<bool> ExistsUserAsync(string email);

        //TO DO: nje metoda per te gjetur nje user me email
    }
}
