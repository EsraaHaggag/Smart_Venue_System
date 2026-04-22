using MediatR;
using Venue_System.Application.Bases;
using Venue_System.Application.Features.Bookings.DTO;

namespace Venue_System.Application.Features.Bookings.Query.GetALLBooking
{
    public record GetALLBookingQuery : IRequest<Response<List<BookingDTo>>>
    {

    }
}
