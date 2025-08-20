using Booking.Application.Common.DTOs;
using Booking.Application.Common.Services;
using Booking_App.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace Booking_App.Services
{
    public class SignalRNotificationService : INotificationService
    {
        private readonly IHubContext<BookingHub> _hubContext;

        public SignalRNotificationService(IHubContext<BookingHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task SendNotificationToOwnerForBookingCreation(Guid ownerId, NotificationDto notification)
        {
            //TO DO: implement logic here 
        }
    }
}
