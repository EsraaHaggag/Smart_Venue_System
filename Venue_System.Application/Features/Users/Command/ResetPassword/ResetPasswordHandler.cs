using MediatR;
using Venue_System.Application.Bases;
using Venue_System.Application.Interfaces.Services;

namespace Venue_System.Application.Features.Users.Command.ResetPassword
{
    public class ResetPasswordHandler : ResponseHandler,
    IRequestHandler<ResetPasswordCommand, Response<string>>
    {
        private readonly IIdentityService _identityService;

        public ResetPasswordHandler(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        public async Task<Response<string>> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
        {
            var result = await _identityService.ResetPasswordWithOtpAsync(
                request.Email,
                request.Otp,
                request.NewPassword
            );

            if (!result)
                return BadRequest<string>("Invalid or expired OTP");

            return Success<string>("Password reset successfully");
        }
    }
}
