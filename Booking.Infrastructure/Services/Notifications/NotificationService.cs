using Microsoft.AspNetCore.SignalR;
using Booking.Application.Common.DTOs;
using Booking.Application.Common.Services.Notifications;
using Booking.Shared.SignalR.Hubs;
using Booking.Shared.SignalR.Clients;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace Booking.Infrastructure.Services.Notifications
{
    public class NotificationService : INotificationService
    {
        private readonly IHubContext<NotificationHub, INotificationClient> _hubContext;
        private readonly ILogger<NotificationService> _logger;
        public NotificationService(IHubContext<NotificationHub, INotificationClient> hubContext, ILogger<NotificationService> logger)
        {
            _hubContext = hubContext;
            _logger = logger;
        }

        public async Task SendToUserAsync(Guid userId, NotificationDto notification)
        {
            try
            {
                await _hubContext.Clients.Groups($"owner-{userId}").ReceiveBookingNotification(notification);
            }
            catch(Exception ex)
            {
                Debug.WriteLine("Error sending notification to user {UserId}.");
            }
        }
    }
}
