using Microsoft.AspNetCore.Identity;
using System.Security.Cryptography;
using Venue_System.Application.Features.Users.DTo;
using Venue_System.Application.Interfaces.Services;
using Venue_System.Infrastructure.Identity;

namespace Venue_System.Infrastructure.Services
{
    public class IdentityService : IIdentityService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IJwtService _jwtService;

        public IdentityService(UserManager<ApplicationUser> userManager, IJwtService jwtService)
        {
            _userManager = userManager;
            _jwtService = jwtService;
        }

        public async Task<(bool Success, Guid UserId, string Error)> CreateUserAsync(
        string email,
        string password,
         string firstName,
        string lastName,
        string phoneNumber,
        string userName)
        {
            var user = new ApplicationUser
            {
                UserName = userName,
                Email = email,
                FirstName = firstName,
                LastName = lastName,
                PhoneNumber = phoneNumber,
                IsActive = true,

            };

            var result = await _userManager.CreateAsync(user, password);

            if (!result.Succeeded)
                return (false, Guid.Empty, string.Join(",", result.Errors.Select(e => e.Description)));

            return (true, user.Id, null);
        }

        public async Task<bool> AddToRoleAsync(Guid userId, string role)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null) return false;

            var result = await _userManager.AddToRoleAsync(user, role);
            return result.Succeeded;
        }
        public async Task<string> GenerateEmailConfirmationTokenAsync(Guid userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null)
                throw new Exception("User not found");
            return await _userManager.GenerateEmailConfirmationTokenAsync(user);
        }

        public async Task<bool> ConfirmEmailAsync(Guid userId, string token)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());

            if (user == null)
                return false;

            var result = await _userManager.ConfirmEmailAsync(user, token);

            return result.Succeeded;
        }
        public async Task<bool> ExistsByEmailAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            return user != null;
        }
        public async Task<UserDto?> FindByEmailAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null) return null;

            return new UserDto
            {
                Email = user.Email,
                ResetPasswordOtp = user.ResetPasswordOtp,
                ResetPasswordOtpExpiry = user.ResetPasswordOtpExpiry
            };
        }
        public async Task<(bool Success, string? Token, string Error)> LoginAsync(string email, string password)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
            {
                return (false, null, "Invalid email or password");
            }

            if (!user.EmailConfirmed)
            {
                return (false, null, "Please confirm your email before logging in");
            }

            var valid = await _userManager.CheckPasswordAsync(user, password);

            if (!valid)
            {
                return (false, null, "Invalid email or password");
            }

            var roles = await _userManager.GetRolesAsync(user);

            if (roles == null || roles.Count == 0)
                return (false, null, "User has no roles assigned");

            var dto = new JwtUserDto
            {
                Id = user.Id,
                Email = user.Email
            };

            var token = _jwtService.GenerateToken(dto, roles);

            return (true, token, null);
        }

        public async Task<string> GeneratePasswordResetTokenAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null) return null;

            return await _userManager.GeneratePasswordResetTokenAsync(user);
        }


        public async Task<bool> ResetPasswordAsync(string email, string token, string newPassword)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null) return false;

            var result = await _userManager.ResetPasswordAsync(user, token, newPassword);
            return result.Succeeded;
        }


        public async Task<string?> GenerateOtpAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null) return null;

            var now = DateTime.UtcNow;

            if (user.LastOtpRequestTime.HasValue &&
                user.LastOtpRequestTime.Value.AddMinutes(1) > now)
            {
                return null;
            }


            if (!user.OtpRequestDate.HasValue || user.OtpRequestDate.Value.Date != now.Date)
            {
                user.OtpRequestDate = now;
                user.OtpRequestCount = 0;
            }

            if (user.OtpRequestCount >= 5)
            {
                return null;
            }


            var otp = RandomNumberGenerator.GetInt32(100000, 999999).ToString();

            user.ResetPasswordOtp = otp;
            user.ResetPasswordOtpExpiry = now.AddMinutes(10);

            user.LastOtpRequestTime = now;
            user.OtpRequestCount++;
            user.OtpRequestDate = now;

            await _userManager.UpdateAsync(user);

            return otp;
        }

        public async Task<bool> ValidateOtpAsync(string email, string otp)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null) return false;

            if (user.ResetPasswordOtp != otp)
                return false;

            if (user.ResetPasswordOtpExpiry < DateTime.UtcNow)
                return false;

            return true;
        }

        public async Task<bool> ResetPasswordWithOtpAsync(string email, string otp, string newPassword)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null) return false;

            if (user.ResetPasswordOtp != otp)
                return false;

            if (user.ResetPasswordOtpExpiry < DateTime.UtcNow)
                return false;

            var remove = await _userManager.RemovePasswordAsync(user);
            if (!remove.Succeeded) return false;

            var add = await _userManager.AddPasswordAsync(user, newPassword);
            if (!add.Succeeded) return false;

            user.ResetPasswordOtp = null;
            user.ResetPasswordOtpExpiry = null;

            await _userManager.UpdateAsync(user);

            return true;
        }


    }
}
