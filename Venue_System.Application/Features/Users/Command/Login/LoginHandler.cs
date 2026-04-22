using MediatR;
using Venue_System.Application.Bases;
using Venue_System.Application.Interfaces.Services;

namespace Venue_System.Application.Features.Users.Command.Login
{
    public class LoginHandler : ResponseHandler, IRequestHandler<LoginCommand, Response<string>>
    {
        private readonly IIdentityService _identityService;

        public LoginHandler(IIdentityService identityService)
        {
            _identityService = identityService;
        }


        public async Task<Response<string>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var result = await _identityService.LoginAsync(request.Email, request.Password);

            if (!result.Success)
                return Unauthorized<string>(result.Error);

            return Success(result.Token);
        }
    }
}
