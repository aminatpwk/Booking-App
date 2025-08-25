using Booking.Application.Common.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Application.Common.Services.Notifications
{
    public interface ITemplateService
    {
        string RenderBookingCreatedTemplate(NotificationDto notification);
        string RenderBookingConfirmedTemplate(NotificationDto notification);
        string RenderBookingCancelledTemplate(NotificationDto notification);
    }
}
