namespace Venue_System.Application.Interfaces.Services
{
    public interface IEmailService
    {
        Task SendAsync(string to, string subject, string content, string templateName);

    }
}
