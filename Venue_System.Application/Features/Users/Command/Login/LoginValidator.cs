using FluentValidation;

namespace Venue_System.Application.Features.Users.Command.Login
{
    public class LoginValidator : AbstractValidator<LoginCommand>
    {
        public LoginValidator()
        {
            RuleFor(x => x.Email)
                .NotNull().WithMessage("Email is required");


            RuleFor(x => x.Password)
                .NotNull().WithMessage("Password is required");

        }
    }
}
