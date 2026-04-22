using MediatR;
using Venue_System.Application.Bases;
using Venue_System.Application.Features.Venues.DTO;

namespace Venue_System.Application.Features.Venues.Query.GetAllVenues
{
    public record GetAllVenuesQuery : IRequest<Response<List<VenueDTo>>>
    {
    }
}
