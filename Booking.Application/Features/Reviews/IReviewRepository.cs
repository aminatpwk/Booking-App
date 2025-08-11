using Booking.Application.Repositories;
using Booking.Domain.Reviews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Application.Features.Reviews
{
    public interface IReviewRepository : IGenericRepository<Review>
    {

    }
}
