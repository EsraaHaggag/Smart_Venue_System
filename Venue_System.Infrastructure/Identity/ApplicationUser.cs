using Microsoft.AspNetCore.Identity;

namespace Venue_System.Infrastructure.Identity
{
    public class ApplicationUser : IdentityUser<Guid>
    {

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsActive { get; set; }
        public string? ResetPasswordOtp { get; set; }
        public DateTime? ResetPasswordOtpExpiry { get; set; }

        public DateTime? LastOtpRequestTime { get; set; }
        public int OtpRequestCount { get; set; } = 0;
        public DateTime? OtpRequestDate { get; set; }
    }

}
