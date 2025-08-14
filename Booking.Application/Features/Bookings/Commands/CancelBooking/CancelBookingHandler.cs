using MediatR;
using Booking.Domain.Bookings;

namespace Booking.Application.Features.Bookings.Commands.CancelBooking
{
    public class CancelBookingHandler : IRequestHandler<CancelBookingCommand, bool>
    {
        private readonly IBookingRepository _bookingRepository;
        public CancelBookingHandler(IBookingRepository bookingRepository)
        {
            _bookingRepository = bookingRepository;
        }

        public async Task<bool> Handle(CancelBookingCommand command, CancellationToken cancellationToken)
        {
            var booking = await _bookingRepository.GetByConfirmationToken(command.Token);
            if (booking == null)
            {
                throw new ArgumentException("Invalid confirmation token!");
            }

            if (booking.Status != BookingStatus.PendingApproval)
            {
                throw new InvalidOperationException("Booking cannot be cancelled in current status!");
            }

            booking.Cancel();
            await _bookingRepository.Update(booking);
            return true;
        }
    }
}
