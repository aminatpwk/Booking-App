using Booking.Application.Common.Model.Email;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/**
 * This interface serves as a layer to communicate between the email service and SendGrid.
 */
namespace Booking.Application.Features.Emails
{
    public interface IEmailService
    {
        Task SendEmailAsync(Email email);
        Task SendEmailWithAttachment(Email email, string attachmentFileName, byte[] attachmentData);
    }
}
