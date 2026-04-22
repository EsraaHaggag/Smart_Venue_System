using MediatR;
using Venue_System.Application.Bases;

namespace Venue_System.Application.Features.Users.Command.Login
{
    public record LoginCommand : IRequest<Response<string>>
    {
        public string Email { get; init; }
        public string Password { get; init; }
    }
}
