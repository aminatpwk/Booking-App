using Booking.Domain.Reviews;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Application.Features.Reviews.Commands.CreateReview
{
    public class CreateReviewCommand : IRequest<Guid>
    {
        public ReviewDto ReviewDto { get; set; }
    }
}
