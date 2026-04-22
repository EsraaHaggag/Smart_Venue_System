using MediatR;
using Venue_System.Application.Bases;

namespace Venue_System.Application.Features.Bookings.Command.CreateBooking
{
    public record CreateBookingCommand : IRequest<Response<Guid>>
    {
        public Guid VenueId { get; init; }
        public DateTime Start { get; init; }
        public DateTime End { get; init; }

    }
}
