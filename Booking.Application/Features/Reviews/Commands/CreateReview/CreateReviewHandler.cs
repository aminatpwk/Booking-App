using MediatR;
using Booking.Domain.Reviews;
using Booking.Application.Features.Users;

namespace Booking.Application.Features.Reviews.Commands.CreateReview
{
    public class CreateReviewHandler : IRequestHandler<CreateReviewCommand, Guid>
    {
        private readonly IReviewRepository _reviewRepository;
        private readonly ICurrentUserService _currentUserService;

        public CreateReviewHandler(IReviewRepository reviewRepository, ICurrentUserService currentUserService)
        {
            _reviewRepository = reviewRepository;
            _currentUserService = currentUserService;
        }

        public async Task<Guid> Handle(CreateReviewCommand request, CancellationToken cancellationToken)
        { 
            var currentUserId = _currentUserService.UserId;

            var review = Review.Create(request.ReviewDto.ApartmentId, currentUserId, request.ReviewDto.Rating, request.ReviewDto.Comment);
            await _reviewRepository.Add(review);
            return review.Id;
        }

    }
}
