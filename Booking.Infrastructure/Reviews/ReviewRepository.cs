using Booking.Application.Features.Reviews;
using Booking.Domain.Reviews;
using Booking.Infrastructure.GenericRepoImpl;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Infrastructure.Reviews
{
    public class ReviewRepository : GenericRepository<Review>, IReviewRepository
    {
        private readonly BookingContext _context;
        public ReviewRepository(BookingContext dbContext) : base(dbContext)
        {
            _context = dbContext;
        }
    }
}
