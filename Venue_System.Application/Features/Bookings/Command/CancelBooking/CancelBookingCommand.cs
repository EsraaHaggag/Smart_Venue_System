using MediatR;
using Venue_System.Application.Bases;

namespace Venue_System.Application.Features.Bookings.Command.CancelBooking
{
    public record CancelBookingCommand(Guid BookingId) : IRequest<Response<Guid>>;
}
