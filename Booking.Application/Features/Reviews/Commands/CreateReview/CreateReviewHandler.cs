using MediatR;
using Booking.Domain.Reviews;
using Booking.Application.Features.Users;
using Booking.Application.Features.Bookings;

namespace Booking.Application.Features.Reviews.Commands.CreateReview
{
    public class CreateReviewHandler : IRequestHandler<CreateReviewCommand, Guid>
    {
        private readonly IReviewRepository _reviewRepository;
        private readonly ICurrentUserService _currentUserService;
        private readonly IBookingRepository _bookingRepository;

        public CreateReviewHandler(IReviewRepository reviewRepository, ICurrentUserService currentUserService, IBookingRepository bookingRepository)
        {
            _reviewRepository = reviewRepository;
            _currentUserService = currentUserService;
            _bookingRepository = bookingRepository;
        }

        public async Task<Guid> Handle(CreateReviewCommand request, CancellationToken cancellationToken)
        { 
            var currentUserId = _currentUserService.UserId;

            var hasCompletedBooking = await _bookingRepository.ExistsAsync(b => b.UserId == currentUserId && b.ApartmentId == request.ReviewDto.ApartmentId && b.End < DateTime.UtcNow, cancellationToken);
            if (!hasCompletedBooking)
            {
                throw new Exception("You can only leave a review after the booking has completed!");
            }

            var alreadyReviewed = await _reviewRepository.ExistsAsync(r => r.UserId == currentUserId && r.ApartmentId == request.ReviewDto.ApartmentId, cancellationToken);
            if (alreadyReviewed)
            {
                throw new Exception("You cannot leave a review more than once per booking!");
            }

            var review = Review.Create(request.ReviewDto.ApartmentId, currentUserId, request.ReviewDto.Rating, request.ReviewDto.Comment);
            await _reviewRepository.Add(review);
            return review.Id;
        }

    }
}
