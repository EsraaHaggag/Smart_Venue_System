using FluentValidation;

namespace Venue_System.Application.Features.Venues.Commnds.ChangePrice
{
    public class ChangePriceValidator : AbstractValidator<ChangePriceCommand>
    {
        public ChangePriceValidator()
        {
            RuleFor(x => x.VenueId)
                    .NotEmpty()
                    .WithMessage("Venue Id is Required");

            RuleFor(x => x.amount)
                    .GreaterThan(0)
                    .WithMessage("Amount Must Greater then Zero");
            RuleFor(x => x.currency)
                    .IsInEnum()
                    .WithMessage("Invalid currency");

        }
    }
}