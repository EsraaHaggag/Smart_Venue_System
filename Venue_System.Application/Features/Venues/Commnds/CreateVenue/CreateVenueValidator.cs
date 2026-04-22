using FluentValidation;

namespace Venue_System.Application.Features.Venues.Commnds.CreateVenue
{
    public class CreateVenueCommandValidator
    : AbstractValidator<CreateVenueCommand>
    {
        public CreateVenueCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(200);

            RuleFor(x => x.Capacity)
                .GreaterThan(0);

            RuleFor(x => x.Description)
                .NotEmpty()
                .MaximumLength(1000);
            RuleFor(x => x.Address)
                .NotEmpty()
                .MaximumLength(400);
            RuleFor(x => x.City)
                .NotEmpty()
                .MaximumLength(100);
            RuleFor(x => x.Country)
                .NotEmpty()
                .MaximumLength(100);
            RuleFor(x => x.Amount)
                .NotEmpty()
                .GreaterThan(0);
            RuleFor(x => x.Currency)
                .NotEmpty();
            RuleFor(x => x.GoogleMapUrl)
                .NotEmpty();
        }
    }
}
