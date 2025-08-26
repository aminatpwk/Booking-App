using Booking.Application.Common.DTOs;
using Booking.Application.Common.DTOs.BookingDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Application.Common.Services.Notifications
{
    public interface ITemplateService
    {
        string RenderBookingCreatedTemplate(BookingNotificationPayload payload);
        string RenderBookingConfirmedTemplate(BookingNotificationPayload payload);
        string RenderBookingCancelledTemplate(BookingNotificationPayload payload);
    }
}
