using MediatR;
using Venue_System.Application.Bases;
using Venue_System.Application.Features.Venues.DTO;

namespace Venue_System.Application.Features.Venues.Query.GetVenueById
{
    public record GetVenueByIdQuery : IRequest<Response<VenueDTo>>
    {
        public Guid VenueId { get; init; }
    }
}
