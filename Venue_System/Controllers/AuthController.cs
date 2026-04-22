using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Venue_System.Application.Features.Users.Command.ForgotPassword;
using Venue_System.Application.Features.Users.Command.Login;
using Venue_System.Application.Features.Users.Command.Register;
using Venue_System.Application.Features.Users.Command.ResetPassword;
using Venue_System.Application.Interfaces.Services;
using Venue_System.Infrastructure.Identity;

namespace Venue_System.API.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;

        private readonly IMediator _mediator;
        private readonly IIdentityService _identityService;

        public AuthController(IMediator mediator, IIdentityService identityService)
        {
            _mediator = mediator;
            _identityService = identityService;
        }

        [HttpGet("confirm-email")]
        public async Task<IActionResult> ConfirmEmail(Guid userId, string token)
        {
            var result = await _identityService.ConfirmEmailAsync(userId, token);

            if (!result)
                return Content("<h2 style='color:red'>Invalid or expired link ❌</h2>", "text/html");

            return Content("<h2 style='color:green'>Email confirmed successfully ✅</h2>", "text/html");
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginCommand command)
        {
            var result = await _mediator.Send(command);

            return StatusCode((int)result.StatusCode, result);
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterCommand command)
        {
            var result = await _mediator.Send(command);

            if (!result.Succeeded)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordCommand command)
        {
            var response = await _mediator.Send(command);
            return StatusCode((int)response.StatusCode, response);
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordCommand command)
        {
            var response = await _mediator.Send(command);
            return StatusCode((int)response.StatusCode, response);
        }
    }
}
