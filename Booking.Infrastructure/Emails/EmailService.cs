using Booking.Application.Features.Emails;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;
using Booking.Application.Common.Exceptions;
using Booking.Application.Common.Model.Email;

namespace Booking.Infrastructure.Emails
{
    public class EmailService(IOptions<EmailSenderOptions> options) : IEmailService
    {
        private readonly EmailSenderOptions _options = options.Value;

        public async Task SendEmailAsync(Email email)
        {
            try
            {
                var client = new SendGridClient(_options.ApiKey);
                var to = new EmailAddress(email.To);
                var from = new EmailAddress(_options.SenderEmail, _options.SenderName);
                var msg = MailHelper.CreateSingleEmail(from, to, email.Subject, email.Body, email.Body);
                var response = await client.SendEmailAsync(msg);

                if (!response.IsSuccessStatusCode)
                {
                    var errorBody = await response.Body.ReadAsStringAsync();
                    throw new EmailSendException($"SendGrid returned {response.StatusCode}: {errorBody}", email.To, (int)response.StatusCode);
                }
            }
            catch (EmailSendException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new EmailSendException($"Unexpected error while sending email to {email.To}", email.To);
            }
        }
    }
}
