using MediatR;
using Venue_System.Application.Bases;
using Venue_System.Application.Comman.DTO;
using Venue_System.Application.Features.DTO;

namespace Venue_System.Application.Features.Venues.Query.GetRulesOfVenue
{
    public class GetRulesOfVenueQuery : IRequest<Response<List<VenueRuleDTo>>>
    {
        public Guid VenueId { get; init; }
        public Guid RuleId { get; init; }
    }

}
