using Booking.Application.Common.DTOs;

namespace Booking.Shared.SignalR.Clients
{
    public interface INotificationClient
    {
        Task ReceiveBookingNotification(NotificationDto notification);

    }
}
