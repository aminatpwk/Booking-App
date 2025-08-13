using Booking.Application.Common.Model.Email;
using Booking.Application.Features.Emails;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Infrastructure.Emails
{
    public class EmailTemplateService : IEmailTemplateService
    {
        private readonly EmailSenderOptions _options;
        public EmailTemplateService(IOptions<EmailSenderOptions> options)
        {
            _options = options.Value;
        }

        public async Task<string> ProcessTemplateAsync(string templateKey, Dictionary<string, object> templateData)
        {
            if(!_options.Templates.TryGetValue(templateKey, out var template))
            {
                throw new ArgumentException($"Email template '{templateKey}' not found");
            }
            var processedSubject = template.Subject;
            var processedBody = template.Body;
            foreach (var data in templateData)
            {
                var placeholder = $"{{{data.Key}}}";
                processedBody = processedBody.Replace(placeholder, data.Value?.ToString() ?? "");
                processedSubject = processedSubject.Replace(placeholder, data.Value?.ToString() ?? "");
            }

            return processedBody;
        }

        public async Task<Email> CreateEmailFromTemplateAsync(string to, string templateKey, Dictionary<string, object> templateData)
        {
            if (!_options.Templates.TryGetValue(templateKey, out var template))
            {
                throw new ArgumentException($"Email template '{templateKey}' not found");
            }

            var processedSubject = template.Subject;
            var processedBody = await ProcessTemplateAsync(templateKey, templateData);
            foreach (var data in templateData)
            {
                var placeholder = $"{{{data.Key}}}";
                processedSubject = processedSubject.Replace(placeholder, data.Value?.ToString() ?? "");
            }

            return new Email
            {
                To = to,
                Subject = processedSubject,
                Body = processedBody
            };
        }
    }
}
