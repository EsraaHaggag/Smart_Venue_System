using MediatR;
using Venue_System.Application.Bases;

namespace Venue_System.Application.Features.Venues.Commnds.Activate
{
    public record ActivateCommand : IRequest<Response<string>>
    {
        public Guid VenueId { get; init; }


    }
}
