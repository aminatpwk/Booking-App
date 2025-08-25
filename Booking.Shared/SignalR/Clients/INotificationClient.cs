using System.Threading.Tasks;

namespace Booking.Shared.SignalR.Clients
{
    public interface INotificationClient
    {
        Task ReceiveBookingNotification(object notification);

    }
}
