using MediatR;
using Venue_System.Application.Bases;
using Venue_System.Application.Comman.Wrappers;
using Venue_System.Application.Features.Bookings.DTO;
using Venue_System.Domain.Enums;

namespace Venue_System.Application.Features.Bookings.Query.GetMyBookings
{
    public class GetMyBookingsQuery : IRequest<Response<PaginatedResult<BookingDTo>>>
    {
        public BookingStatus? Status { get; set; }

        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
