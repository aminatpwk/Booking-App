using Booking.Application.Common.DTOs;
using Booking.Application.Common.Services;
using Booking.Application.Features.Apartments;
using Booking.Application.Features.Bookings.Commands.ConfirmBooking;
using Booking.Application.Features.Emails;
using Booking.Application.Features.Users;
using Booking.Domain.Bookings;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Booking.Application.Features.Bookings.Commands.CancelBooking
{
    public class CancelBookingHandler : IRequestHandler<CancelBookingCommand, bool>
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly ICurrentUserService _currentUserService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IApartmentRepository _apartmentRepository;
        private readonly IEmailTemplateService _emailTemplateService;
        private readonly IEmailService _emailService;
        private readonly ILogger<ConfirmBookingHandler> _logger;
        private readonly INotificationService _notificationService;

        public CancelBookingHandler(IBookingRepository bookingRepository, ICurrentUserService currentUserService, IHttpContextAccessor httpContextAccessor, IApartmentRepository apartmentRepository, IEmailTemplateService emailTemplateService, IEmailService emailService, ILogger<ConfirmBookingHandler> logger, INotificationService notificationService)
        {
            _bookingRepository = bookingRepository;
            _currentUserService = currentUserService;
            _httpContextAccessor = httpContextAccessor;
            _apartmentRepository = apartmentRepository;
            _emailTemplateService = emailTemplateService;
            _emailService = emailService;
            _logger = logger;
            _notificationService = notificationService;
        }

        public async Task<bool> Handle(CancelBookingCommand command, CancellationToken cancellationToken)
        {
            var booking = await _bookingRepository.GetByConfirmationToken(command.Token);
            if (booking == null)
            {
                throw new ArgumentException("Invalid confirmation token!");
            }
            if(DateTime.UtcNow > booking.ConfirmationTokenExpiration)
            {
                throw new Exception("The confirmation token has expired!");
            }

            if (booking.Status != BookingStatus.PendingApproval)
            {
                throw new InvalidOperationException("Booking cannot be cancelled in current status!");
            }

            var apartment = await _apartmentRepository.GetById(booking.ApartmentId);

            booking.Cancel();
            await _bookingRepository.Update(booking);
            await SendEmailAfterCancellation(booking, apartment);

            var notificationDto = new NotificationDto
            {
                BookingId = booking.Id,
                ApartmentId = booking.ApartmentId,
                CheckIn = booking.Start,
                CheckOut = booking.End,
                GuestId = booking.UserId,
                CreatedAt = DateTime.UtcNow,
                Status = BookingStatus.Confirmed
            };

            await _notificationService.SendNotificationToOwnerForBookingCreation(apartment.OwnerId, notificationDto);

            return true;
        }

        private async Task SendEmailAfterCancellation(BookingEntity booking, Apartment apartment)
        {
            try
            {
                var userEmail = _currentUserService.Email;
                if (userEmail == null)
                {
                    throw new Exception("E-mail not found!");
                }

                var request = _httpContextAccessor.HttpContext?.Request;
                var baseUrl = $"{request.Scheme}://{request?.Host}";

                var templateData = new Dictionary<string, object>
                {
                    {"StartDate", booking.Start.ToString("yyyy-MM-dd") },
                    {"EndDate", booking.End.ToString("yyyy-MM-dd") },
                    {"TotalPrice", booking.TotalPrice.ToString("C") },
                    {"ApartmentName", apartment?.Name ?? "Name of apartment not available" },
                    {"ApartmentAddress", apartment?.Address ?? "Address not available" },
                    {"CancellationDate", DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm") },
                };

                var email = await _emailTemplateService.CreateEmailFromTemplateAsync(userEmail, "BookingCancelled", templateData);
                await _emailService.SendEmailAsync(email);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to send email!");
            }
        }
    }
}
