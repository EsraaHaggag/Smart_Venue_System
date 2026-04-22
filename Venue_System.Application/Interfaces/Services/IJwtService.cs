using Venue_System.Application.Features.Users.DTo;

namespace Venue_System.Application.Interfaces.Services
{
    public interface IJwtService
    {
        string GenerateToken(JwtUserDto user, IList<string> roles);
    }
}
