using Booking.Application.Features.Reviews;
using Booking.Domain.Reviews;
using Booking.Infrastructure.GenericRepoImpl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Infrastructure.Reviews
{
    public class ReviewRepository : GenericRepository<Review>, IReviewRepository
    {
        public ReviewRepository(BookingContext context) : base(context)
        {
        }
    }
}
