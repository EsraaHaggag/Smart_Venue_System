using Venue_System.Application.Features.Users.DTo;

namespace Venue_System.Application.Interfaces.Services
{
    public interface IIdentityService
    {
        Task<(bool Success, Guid UserId, string Error)> CreateUserAsync(
        string email,
        string password,
        string firstName,
        string lastName,
        string phoneNumber,
        string userName);

        Task<bool> AddToRoleAsync(Guid userId, string role);
        Task<string> GenerateEmailConfirmationTokenAsync(Guid userId);

        Task<bool> ConfirmEmailAsync(Guid userId, string token);

        Task<bool> ExistsByEmailAsync(string email);
        Task<(bool Success, string? Token, string Error)> LoginAsync(string email, string password);

        Task<bool> ResetPasswordAsync(string email, string token, string newPassword);

        Task<string> GeneratePasswordResetTokenAsync(string email);

        Task<UserDto?> FindByEmailAsync(string email);
        Task<string?> GenerateOtpAsync(string email);
        Task<bool> ValidateOtpAsync(string email, string otp);
        Task<bool> ResetPasswordWithOtpAsync(string email, string otp, string newPassword);
    }
}
