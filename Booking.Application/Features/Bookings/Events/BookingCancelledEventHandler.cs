using MediatR;
using Booking.Application.Common.Services.Notifications;
using Microsoft.Extensions.Logging;
using Booking.Application.Common.Events.Bookings;
using Booking.Application.Common.DTOs.BookingDTOs;
using Booking.Domain.Bookings;
using Booking.Application.Common.DTOs;
using Booking.Application.Common.Enums;

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
            var payload = new BookingNotificationPayload
            {
                BookingId = domainEvent.BookingId,
                ApartmentId = domainEvent.ApartmentId,
                ApartmentName = domainEvent.ApartmentName,
                CheckIn = domainEvent.CheckIn,
                CheckOut = domainEvent.CheckOut,
                Status = BookingStatus.Cancelled
            };

            var notification = new NotificationDto
            {
                Type = NotificationTypes.BookingCancelled,
                Message = _templateService.RenderBookingCancelledTemplate(payload),
                CreatedAt = domainEvent.DateOccurred,
                Payload = payload
            };

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
