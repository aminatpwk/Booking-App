
using Booking.Application.Features.Users;
using MediatR;

namespace Booking.Application.Features.Owners.Queries.CheckOwnerProfile
{
    public class CheckOwnerProfileQueryHandler : IRequestHandler<CheckOwnerProfileQuery, CheckOwnerProfileResult>
    {
        private readonly IOwnerRepository _ownerRepository;
        private readonly ICurrentUserService _currentUserService;
        public CheckOwnerProfileQueryHandler(IOwnerRepository ownerRepository, ICurrentUserService currentUserService)
        {
            _ownerRepository = ownerRepository;
            _currentUserService = currentUserService;
        }

        public async Task<CheckOwnerProfileResult> Handle(CheckOwnerProfileQuery request, CancellationToken cancellationToken)
        {
            var userId = _currentUserService.UserId;
            if (userId == Guid.Empty)
            {
                throw new UnauthorizedAccessException("User not authenticated!");
            }

            var hasProfile = await _ownerRepository.UserAlreadyHasOwnerProfile(userId, cancellationToken);
            return new CheckOwnerProfileResult
            {
                HasOwnerProfile = hasProfile
            };
        }
    }
}
