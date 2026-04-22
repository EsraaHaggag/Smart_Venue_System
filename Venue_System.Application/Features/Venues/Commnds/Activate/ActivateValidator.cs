using FluentValidation;

namespace Venue_System.Application.Features.Venues.Commnds.Activate
{
    public class ActivateValidator : AbstractValidator<ActivateCommand>
    {
        public ActivateValidator()
        {
            RuleFor(x => x.VenueId)
                    .NotEmpty()
                    .WithMessage("Venue Id is Required");

        }
    }
}
