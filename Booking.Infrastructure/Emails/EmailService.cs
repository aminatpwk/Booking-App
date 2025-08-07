using Booking.Application.Features.Emails;
using Booking.Application.Common.Model;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail; 

namespace Booking.Infrastructure.Emails
{
    public class EmailService(IOptions<EmailSenderOptions> options) : IEmailService
    {
        private readonly EmailSenderOptions _options = options.Value;

        public async Task SendEmailAsync(Email email)
        {
            var client = new SendGridClient(_options.ApiKey);
            var to = new EmailAddress(email.To);
            var from = new EmailAddress(_options.SenderEmail, _options.SenderName);
            var msg = MailHelper.CreateSingleEmail(from, to, email.Subject, email.Body, email.Body);
            var response = await client.SendEmailAsync(msg);
        }
    }
}
