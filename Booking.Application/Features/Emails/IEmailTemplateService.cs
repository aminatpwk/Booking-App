using Booking.Application.Common.Model.Email;

namespace Booking.Application.Features.Emails
{
    /**
     * This interface serves for managing the templates before calling the email service.
     */
    public interface IEmailTemplateService
    {
        Task<string> ProcessTemplateAsync(string templateKey, Dictionary<string, object> templateData);
        Task<Email> CreateEmailFromTemplateAsync(string to, string templateKey, Dictionary<string, object> templateData);
    }
}
