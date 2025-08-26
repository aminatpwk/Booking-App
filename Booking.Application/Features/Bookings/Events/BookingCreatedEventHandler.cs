using MediatR;
using Booking.Application.Common.Events.Bookings;
using Booking.Application.Common.Services.Notifications;
using Booking.Application.Common.DTOs;
using Microsoft.Extensions.Logging;
using Booking.Domain.Bookings;
using Booking.Application.Common.DTOs.BookingDTOs;
using Booking.Application.Common.Enums;

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
            var payload = new BookingNotificationPayload
            {
                BookingId = domainEvent.BookingId,
                ApartmentId = domainEvent.ApartmentId,
                ApartmentName = domainEvent.ApartmentName,
                CheckIn = domainEvent.CheckIn,
                CheckOut = domainEvent.CheckOut,
                Status = BookingStatus.PendingApproval
            };

            var notification = new NotificationDto
            {
                Type = NotificationTypes.BookingCreated,
                Message = _templateService.RenderBookingCreatedTemplate(payload),
                CreatedAt = domainEvent.DateOccurred,
                Payload = payload
            };

            try
            {
                await _notificationService.SendToUserAsync(domainEvent.OwnerId, notification);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error sending booking created notification for BookingId: {BookingId}", domainEvent.BookingId);
            }
        }
    }
}
