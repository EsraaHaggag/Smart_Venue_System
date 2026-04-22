using FluentValidation;
namespace Venue_System.Application.Features.Venues.Commnds.RemoveVenue
{
    public class RemoveVenueValidator : AbstractValidator<RemoveVenueCommand>
    {
        public RemoveVenueValidator()
        {
            RuleFor(x => x.VenueId)
                    .NotEmpty()
                    .WithMessage("Venue Id is Required");
        }
    }
}
