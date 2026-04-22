using MediatR;
using Venue_System.Application.Bases;

namespace Venue_System.Application.Features.Venues.Commnds.Deactivate
{
    public record DeactivateCommand : IRequest<Response<string>>
    {
        public Guid VenueId { get; init; }


    }
}
