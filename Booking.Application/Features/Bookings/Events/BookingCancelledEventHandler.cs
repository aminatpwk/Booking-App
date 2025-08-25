using MediatR;
using Booking.Application.Common.Services.Notifications;
using Microsoft.Extensions.Logging;
using Booking.Application.Common.Events.Bookings;
using Booking.Application.Common.DTOs;

namespace Booking.Application.Features.Bookings.Events
{
    public class BookingCancelledEventHandler : INotificationHandler<BookingCancelledEvent>
    {
        private readonly ILogger<BookingCancelledEventHandler> _logger;
        private readonly INotificationService _notificationService;
        private readonly ITemplateService _templateService;

        public BookingCancelledEventHandler(ILogger<BookingCancelledEventHandler> logger, INotificationService notificationService, ITemplateService templateService)
        {
            _logger = logger;
            _notificationService = notificationService;
            _templateService = templateService;
        }

        public async Task Handle(BookingCancelledEvent domainEvent, CancellationToken cancellationToken)
        {
            var notification = new NotificationDto
            {
                BookingId = domainEvent.BookingId,
                ApartmentId = domainEvent.ApartmentId,
                ApartmentName = domainEvent.ApartmentName,
                Status = Domain.Bookings.BookingStatus.Cancelled
            };
            var message = _templateService.RenderBookingCancelledTemplate(notification);
            notification.Message = message;
            try
            {
                await _notificationService.SendToUserAsync(domainEvent.OwnerId, notification);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error sending booking cancelled notification for BookingId: {BookingId}", domainEvent.BookingId);
            }
        }
    }
}
