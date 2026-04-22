using FluentValidation;
using Venue_System.Application.Features.Venues.Commnds.AddWorkingHours;

namespace Venue_System.Application.Features.Venues.Commnds.UpdateWorkingHours
{
    public class UpdateWorkingHoursValidator : AbstractValidator<AddWorkingHoursCommand>
    {
        public UpdateWorkingHoursValidator()
        {
            RuleFor(x => x.VenueId)
                    .NotEmpty()
                    .WithMessage("Venue Id is Required");
            RuleFor(x => x.OpenFrom)
                    .NotEmpty()
                    .WithMessage("OpenFrom is Required");
            RuleFor(x => x.OpenTo)
                    .NotEmpty()
                    .WithMessage("OpenTo is Required");
            RuleFor(x => x.IsClosed)
                    .NotEmpty()
                    .WithMessage("IsClosed is Required");
        }


    }
}
