using Booking.Domain.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Application.Common.Services
{
    public interface IElasticService
    {
        Task CreateIndexIfNotExistsAsync(string indexName);
        //add or update document (e.g., booking, user, etc.)
        Task<bool> AddOrUpdate(User user);
        //add or update user bulk
        Task<bool> AddOrUpdateBullk(IEnumerable<User> users, string indexName);
        Task<User> Get(string key);
        Task<List<User>> GetAll();
        Task<bool> Remove(string key);
        Task<long?> RemoveAll();
    }
}
