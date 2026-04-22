using FluentValidation;

namespace Venue_System.Application.Features.Venues.Commnds.UpdateVenue
{
    public class UpdateVenueValidator
        : AbstractValidator<UpdateVenueCommand>
    {
        public UpdateVenueValidator()
        {
            RuleFor(x => x.Name)
                    .NotEmpty()
                    .MaximumLength(100);
            RuleFor(x => x.Id)
                    .NotEmpty()
                    .WithMessage("Venue Id is Required");

            RuleFor(x => x.Capacity)
                    .GreaterThan(0);
            RuleFor(x => x.Description)
                    .NotEmpty()
                    .MaximumLength(100);
            RuleFor(x => x.Address)
                    .NotEmpty()
                    .MaximumLength(100);
            RuleFor(x => x.City)
                    .NotEmpty();
            RuleFor(x => x.Country)
                    .NotEmpty();
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
