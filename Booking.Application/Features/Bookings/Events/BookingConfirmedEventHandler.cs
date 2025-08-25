using Booking.Application.Common.Events.Bookings;
using MediatR;
using Microsoft.Extensions.Logging;
using Booking.Application.Common.Services.Notifications;
using Booking.Application.Common.DTOs;
using Booking.Domain.Bookings;

namespace Booking.Application.Features.Bookings.Events
{
    public class BookingConfirmedEventHandler : INotificationHandler<BookingConfirmedEvent>
    {
        private readonly ILogger<BookingConfirmedEventHandler> _logger;
        private readonly INotificationService _notificationService;
        private readonly ITemplateService _templateService;

        public BookingConfirmedEventHandler(ILogger<BookingConfirmedEventHandler> logger, INotificationService notificationService, ITemplateService templateService)
        {
            _logger = logger;
            _notificationService = notificationService;
            _templateService = templateService;
        }

        public async Task Handle(BookingConfirmedEvent domainEvent, CancellationToken cancellationToken)
        {
            var notification = new NotificationDto
            {
                BookingId = domainEvent.BookingId,
                ApartmentId = domainEvent.ApartmentId,
                ApartmentName = domainEvent.ApartmentName,
                CheckIn = domainEvent.CheckIn,
                CheckOut = domainEvent.CheckOut,
                CreatedAt = domainEvent.DateOccurred,
                Status = BookingStatus.Confirmed
            };

            var message = _templateService.RenderBookingConfirmedTemplate(notification);
            notification.Message = message;

            try
            {
                await _notificationService.SendToUserAsync(domainEvent.OwnerId, notification);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error sending booking confirmed notification for BookingId: {BookingId}", domainEvent.BookingId);
            }
        }
    }
}
