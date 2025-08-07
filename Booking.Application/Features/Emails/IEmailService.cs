using Booking.Application.Common.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Application.Features.Emails
{
    public interface IEmailService
    {
        Task SendEmailAsync(Email email);
    }
}
