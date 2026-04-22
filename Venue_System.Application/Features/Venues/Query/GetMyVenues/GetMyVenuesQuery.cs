using MediatR;
using Venue_System.Application.Bases;
using Venue_System.Application.Features.Venues.DTO;

namespace Venue_System.Application.Features.Venues.Query.GetMyVenues
{
    public record GetMyVenuesQuery : IRequest<Response<List<VenueDTo>>>
    {
    }
}
