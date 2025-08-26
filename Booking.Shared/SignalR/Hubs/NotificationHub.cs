using Booking.Shared.SignalR.Clients;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using System.Security.Claims;


namespace Booking.Shared.SignalR.Hubs
{
    [Authorize(Roles="Owner")]
    public class NotificationHub : Hub<INotificationClient>
    {
        private readonly ILogger<NotificationHub> _logger;
        public NotificationHub(ILogger<NotificationHub> logger)
        {
            _logger = logger;
        }

        public override async Task OnConnectedAsync()
        {
            var (userId, role) = GetUserIdentifiers();
            if(role == "Owner" && userId != null)
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, $"owner-{userId}");
            }
            else
            {
                _logger.LogWarning("A non-owner user attempted to connect to NotificationHub. ConnectionId: {ConnectionId}", Context.ConnectionId);
            }
            
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            var (userId, role) = GetUserIdentifiers();  
            if(role == "Owner" && userId != null)
            {
                await Groups.RemoveFromGroupAsync(Context.ConnectionId, $"owner-{userId}");
            }

            await base.OnDisconnectedAsync(exception);
        }

        private (string? userId, string? role) GetUserIdentifiers()
        {
            var userId = Context.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var role = Context.User?.FindFirst(ClaimTypes.Role)?.Value;
            return (userId, role);
        }
    }
}
