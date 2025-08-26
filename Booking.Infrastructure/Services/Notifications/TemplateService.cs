using Booking.Application.Common.DTOs;
using Booking.Application.Common.DTOs.BookingDTOs;
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

        public string RenderBookingCreatedTemplate(BookingNotificationPayload payload)
        {
            var template = _configuration["Notifications:BookingCreated"];
            return template.Replace("{ApartmentName}", payload.ApartmentName)
            .Replace("{StartDate}", payload.CheckIn.ToString("dd-MM-yyyy"))
                .Replace("{EndDate}", payload.CheckOut.ToString("dd-MM-yyyy"));
        }

        public string RenderBookingConfirmedTemplate(BookingNotificationPayload payload)
        {
            var template = _configuration["Notifications:BookingConfirmed"];
            return template.Replace("{ApartmentName}", payload.ApartmentName);
        }

        public string RenderBookingCancelledTemplate(BookingNotificationPayload payload)
        {
            var template = _configuration["Notifications:BookingCancelled"];
            return template.Replace("{ApartmentName}", payload.ApartmentName);
        }
    }
}
