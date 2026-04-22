using MediatR;
using Venue_System.Application.Bases;
using Venue_System.Application.Interfaces.Services;

namespace Venue_System.Application.Features.Users.Command.ForgotPassword
{
    public class ForgotPasswordHandler : ResponseHandler,
    IRequestHandler<ForgotPasswordCommand, Response<string>>
    {
        private readonly IIdentityService _identityService;
        private readonly IEmailService _emailService;

        public ForgotPasswordHandler(IIdentityService identityService, IEmailService emailService)
        {
            _identityService = identityService;
            _emailService = emailService;
        }

        public async Task<Response<string>> Handle(ForgotPasswordCommand request, CancellationToken cancellationToken)
        {
            var otp = await _identityService.GenerateOtpAsync(request.Email);

            var emailContent = $@"
                <p>You have requested to reset your password. Please use the following code:</p>
                <center><div class='otp-code'>{otp}</div></center>
                <p>This code is valid for 120 minutes.</p>";
            if (otp != null)
            {
                await _emailService.SendAsync(
                    request.Email,
                    "Reset Password Request",
                    emailContent,
                    "otp-template"
                );
            }

            return Success<string>("If this email exists, a reset code has been sent.");
        }
    }
}

