using FluentValidation;

namespace Venue_System.Application.Features.Bookings.Command.CancelBooking
{
    public class CancelBookingValidator : AbstractValidator<CancelBookingCommand>
    {
        public CancelBookingValidator()
        {
            RuleFor(x => x.BookingId)
                    .NotEmpty()
                    .WithMessage("Booking Id is Required");

        }
    }
}
