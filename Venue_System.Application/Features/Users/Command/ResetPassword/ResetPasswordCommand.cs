using MediatR;
using Venue_System.Application.Bases;

namespace Venue_System.Application.Features.Users.Command.ResetPassword
{
    public record ResetPasswordCommand : IRequest<Response<string>>
    {
        public string Email { get; init; }
        public string Otp { get; init; }
        public string NewPassword { get; init; }
    }
}
