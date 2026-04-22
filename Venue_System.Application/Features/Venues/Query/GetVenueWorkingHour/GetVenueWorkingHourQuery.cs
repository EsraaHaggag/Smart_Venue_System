using MediatR;
using Venue_System.Application.Bases;
using Venue_System.Application.Comman.DTO;
using Venue_System.Application.Features.DTO;

namespace Venue_System.Application.Features.Venues.Query.GetVenueWorkingHour
{
    public class GetVenueWorkingHourQuery : IRequest<Response<List<WorkingHourDTo>>>
    {
        public Guid VenueId { get; init; }

    }
}
