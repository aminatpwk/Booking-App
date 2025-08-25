using Booking.Shared.SignalR.Clients;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using System.Security.Claims;
using System.Threading.Tasks;


namespace Booking.Shared.SignalR.Hubs
{
    public class NotificationHub : Hub<INotificationClient>
    {
        private readonly ILogger<NotificationHub> _logger;
        public NotificationHub(ILogger<NotificationHub> logger)
        {
            _logger = logger;
        }

        public override async Task OnConnectedAsync()
        {
            var userId = Context.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var userRole = Context.User?.FindFirst(ClaimTypes.Role)?.Value;
            if(userRole == "Owner")
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, "Owners");
                await Groups.AddToGroupAsync(Context.ConnectionId, $"owner-{userId}");
            }
            else
            {
                Debug.WriteLine("A non-owner user connected to NotificationHub.");
            }
                await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            var userId = Context.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var userRole = Context.User?.FindFirst(ClaimTypes.Role)?.Value;

            if (userRole == "Owner")
            {
                await Groups.RemoveFromGroupAsync(Context.ConnectionId, "Owners");
                await Groups.RemoveFromGroupAsync(Context.ConnectionId, $"owner-{userId}");
            }

            await base.OnDisconnectedAsync(exception);
        }
    }
}
