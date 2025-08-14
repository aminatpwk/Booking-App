using MediatR;
using Booking.Domain.Bookings;

namespace Booking.Application.Features.Bookings.Commands.ConfirmBooking
{
    public class ConfirmBookingHandler : IRequestHandler<ConfirmBookingCommand, bool>
    {
        private readonly IBookingRepository _bookingRepository;
        public ConfirmBookingHandler(IBookingRepository bookingRepository)
        {
            _bookingRepository = bookingRepository;
        }

        public async Task<bool> Handle(ConfirmBookingCommand command, CancellationToken cancellationToken)
        {
            var booking = await _bookingRepository.GetByConfirmationToken(command.Token);
            if(booking == null)
            {
                throw new ArgumentException("Invalid confirmation token!");
            }
            if(DateTime.UtcNow > booking.ConfirmationTokenExpiration)
            {
                throw new Exception("The confirmation token has expired!");
            }

            if(booking.Status != BookingStatus.PendingApproval)
            {
                throw new InvalidOperationException("Booking cannot be confirmed in current status!"); 
            }

            booking.Confirm();
            await _bookingRepository.Update(booking);
            return true;
        }
    }
}
