using Booking.Shared.SignalR.Clients;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Security.Claims;
using System.Threading.Tasks;


namespace Booking.Shared.SignalR.Hubs
{
    public class NotificationHub : Hub<INotificationClient>
    {
        public override async Task OnConnectedAsync()
        {
            var userId = Context.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var userRole = Context.User?.FindFirst(ClaimTypes.Role)?.Value;
            if(userRole == "Owner")
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, "Owners");
                await Groups.AddToGroupAsync(Context.ConnectionId, $"owner-{userId}");
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
