using MediatR;
using Venue_System.Application.Bases;

namespace Venue_System.Application.Features.Venues.Commnds.RemoveRule
{
    public record RemoveRuleCommand : IRequest<Response<string>>
    {
        public Guid RuleId { get; init; }
        public Guid VenueId { get; init; }

    }
}
