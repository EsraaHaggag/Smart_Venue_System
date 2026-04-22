
using FluentValidation;

namespace Venue_System.Application.Features.Bookings.Command.CreateBooking
{
    public class CreateBookingValidator : AbstractValidator<CreateBookingCommand>
    {
        public CreateBookingValidator()
        {
            RuleFor(x => x.VenueId)
                    .NotEmpty()
                    .WithMessage("Venue Id is Required");
            RuleFor(x => x.Start)
                    .NotEmpty()
                    .WithMessage("Start Time is Required");
            RuleFor(x => x.End)
                    .NotEmpty()
                    .WithMessage("End Time is Required");

        }
    }
}
