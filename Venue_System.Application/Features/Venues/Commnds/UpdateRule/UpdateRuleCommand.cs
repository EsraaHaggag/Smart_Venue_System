using MediatR;
using Venue_System.Application.Bases;

namespace Venue_System.Application.Features.Venues.Commnds.UpdateRule
{
    public record UpdateRuleCommand : IRequest<Response<string>>
    {
        public Guid VenueId { get; init; }
        public Guid RuleId { get; init; }
        public string RuleText { get; init; }
        public bool IsMandatory { get; init; }

    }
}
