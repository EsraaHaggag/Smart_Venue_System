using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using Venue_System.Application.Interfaces.Services;

namespace Venue_System.Infrastructure.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public Guid UserId
        {
            get
            {
                var userIdString = _httpContextAccessor.HttpContext?
                    .User?
                    .FindFirst(ClaimTypes.NameIdentifier)?.Value;

                if (string.IsNullOrEmpty(userIdString) || !Guid.TryParse(userIdString, out var userId))
                    throw new UnauthorizedAccessException("User identity not found in token.");

                return userId;
            }
        }

        public string? Role
        {
            get
            {
                return _httpContextAccessor.HttpContext?.User?
                    .FindFirst(ClaimTypes.Role)?.Value;
            }
        }
    }
}