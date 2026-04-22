using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;
using Venue_System.Application.Interfaces.Services;

namespace Venue_System.Infrastructure.Services.Email
{
    public class EmailService : IEmailService
    {
        private readonly EmailSettings _settings;

        public EmailService(IOptions<EmailSettings> options)
        {
            _settings = options.Value;
        }

        //public async Task SendAsync(string to, string subject, string confirmationLink)
        //{
        //    var path = Path.Combine(
        //        Directory.GetCurrentDirectory(),
        //        "Email",
        //        "confirm-email.html");

        //    var body = await File.ReadAllTextAsync(path);

        //    body = body.Replace("{{link}}", confirmationLink);

        //    using var smtp = new SmtpClient(_settings.Host)
        //    {
        //        Port = _settings.Port,
        //        Credentials = new NetworkCredential(_settings.Email, _settings.Password),
        //        EnableSsl = _settings.EnableSsl,
        //    };

        //    using var mail = new MailMessage(_settings.Email, to, subject, body)
        //    {
        //        IsBodyHtml = true
        //    };

        //    await smtp.SendMailAsync(mail);
        //}

        public async Task SendAsync(string to, string subject, string content, string templateName)
        {

            var path = Path.Combine(
                Directory.GetCurrentDirectory(),
                "Email",
                $"{templateName}.html");

            if (!File.Exists(path))
                throw new FileNotFoundException($"Email template '{templateName}.html' not found at {path}");

            var body = await File.ReadAllTextAsync(path);


            body = body.Replace("{{subject}}", subject);
            body = body.Replace("{{content}}", content);
            body = body.Replace("{{date}}", DateTime.Now.Year.ToString());

            using var smtp = new SmtpClient(_settings.Host)
            {
                Port = _settings.Port,
                Credentials = new NetworkCredential(_settings.Email, _settings.Password),
                EnableSsl = _settings.EnableSsl,
            };

            using var mail = new MailMessage(_settings.Email, to, subject, body)
            {
                IsBodyHtml = true
            };

            await smtp.SendMailAsync(mail);
        }
    }
}
