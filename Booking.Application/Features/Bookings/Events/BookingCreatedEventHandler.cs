using MediatR;
using Booking.Application.Common.Events.Bookings;
using Booking.Application.Common.Services.Notifications;
using Booking.Application.Common.DTOs;
using Microsoft.Extensions.Logging;
using Booking.Application.Common.Events;

namespace Booking.Application.Features.Bookings.Events
{
    public class BookingCreatedEventHandler :  INotificationHandler<BookingCreatedEvent>
    {
        private readonly INotificationService _notificationService;
        private readonly ITemplateService _templateService;
        private readonly ILogger<BookingCreatedEventHandler> _logger;

        public BookingCreatedEventHandler(INotificationService notificationService, ITemplateService templateService, ILogger<BookingCreatedEventHandler> logger)
        {
            _notificationService = notificationService;
            _templateService = templateService;
            _logger = logger;
        }

        public async Task Handle(BookingCreatedEvent domainEvent, CancellationToken cancellationToken)
        {
            var notification = new NotificationDto
            {
                BookingId = domainEvent.BookingId,
                OwnerId = domainEvent.OwnerId,
                ApartmentName = domainEvent.ApartmentName,
                CreatedAt = domainEvent.DateOccurred
            };

            var message = _templateService.RenderBookingCreatedTemplate(notification);
            notification.Message = message;

            await _notificationService.SendToUserAsync(domainEvent.OwnerId, notification);

            try
            {

            }catch(Exception ex)
            {
                _logger.LogError(ex, "Error sending booking created notification for BookingId: {BookingId}", domainEvent.BookingId);
            }
        }
    }
}
