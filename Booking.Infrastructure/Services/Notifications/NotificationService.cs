using Microsoft.AspNetCore.SignalR;
using Booking.Application.Common.DTOs;
using Booking.Application.Common.Services.Notifications;
using Booking.Shared.SignalR.Hubs;
using Booking.Shared.SignalR.Clients;

namespace Booking.Infrastructure.Services.Notifications
{
    public class NotificationService : INotificationService
    {
        private readonly IHubContext<NotificationHub, INotificationClient> _hubContext;
        public NotificationService(IHubContext<NotificationHub, INotificationClient> hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task SendToUserAsync(Guid userId, NotificationDto notification)
        {
            await _hubContext.Clients.User(userId.ToString()).ReceiveBookingNotification(notification);
        }
    }
}
