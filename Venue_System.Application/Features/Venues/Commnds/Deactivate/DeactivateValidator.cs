using FluentValidation;

namespace Venue_System.Application.Features.Venues.Commnds.Deactivate
{
    public class DeactivateValidator : AbstractValidator<DeactivateCommand>
    {
        public DeactivateValidator()
        {
            RuleFor(x => x.VenueId)
                    .NotEmpty()
                    .WithMessage("Venue Id is Required");

        }
    }
}
