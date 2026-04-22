using MediatR;
using Venue_System.Application.Bases;

namespace Venue_System.Application.Features.Venues.Commnds.RemoveVenue
{
    public record RemoveVenueCommand : IRequest<Response<string>>
    {
        public Guid VenueId { get; init; }
    }
}
