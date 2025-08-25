using Booking.Application.Common.DTOs;

namespace Booking.Application.Common.Services.Notifications
{
    public interface INotificationService
    {
        Task SendToUserAsync(Guid userId, NotificationDto notification);
    }
}
