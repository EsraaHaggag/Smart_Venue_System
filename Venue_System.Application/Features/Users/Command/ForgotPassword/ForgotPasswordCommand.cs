using MediatR;
using Venue_System.Application.Bases;

namespace Venue_System.Application.Features.Users.Command.ForgotPassword
{
    public record ForgotPasswordCommand : IRequest<Response<string>>
    {
        public string Email { get; init; }
    }
}
