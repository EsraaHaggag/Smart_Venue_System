namespace Venue_System.Application.Features.Users.DTo
{
    public class UserDto
    {
        public string Email { get; set; }
        public string? ResetPasswordOtp { get; set; }
        public DateTime? ResetPasswordOtpExpiry { get; set; }
    }
}
