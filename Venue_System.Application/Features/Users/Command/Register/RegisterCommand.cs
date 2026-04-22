using MediatR;
using Venue_System.Application.Bases;
using Venue_System.Domain.Enums;

namespace Venue_System.Application.Features.Users.Command.Register
{
    public record RegisterCommand : IRequest<Response<Guid>>
    {
        public string FirstName { get; init; }
        public string Username { get; init; }
        public string LastName { get; init; }
        public string Email { get; init; }
        public string Password { get; init; }
        public string ConfirmPassword { get; init; }
        public string PhoneNumber { get; init; }
        public UserRule Role { get; init; }
    }
}
