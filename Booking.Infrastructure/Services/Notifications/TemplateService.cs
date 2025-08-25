using Booking.Application.Common.DTOs;
using Booking.Application.Common.Services.Notifications;
using Microsoft.Extensions.Configuration;

namespace Booking.Infrastructure.Services.Notifications
{
    public class TemplateService : ITemplateService
    {
        private readonly IConfiguration _configuration;
        public TemplateService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string RenderBookingCreatedTemplate(NotificationDto notification)
        {
            var template = _configuration["Notifications:BookingCreated"];
            return template.Replace("{ApartmentName}", notification.ApartmentName)
            .Replace("{StartDate}", notification.CheckIn.ToString("dd-MM-yyyy"))
                .Replace("{EndDate}", notification.CheckOut.ToString("dd-MM-yyyy"));
        }

        public string RenderBookingConfirmedTemplate(NotificationDto notification)
        {
            var template = _configuration["Notifications:BookingConfirmed"];
            return template.Replace("{ApartmentName}", notification.ApartmentName);
        }

        public string RenderBookingCancelledTemplate(NotificationDto notification)
        {
            var template = _configuration["Notifications:BookingCancelled"];
            return template.Replace("{ApartmentName}", notification.ApartmentName);
        }
    }
}
