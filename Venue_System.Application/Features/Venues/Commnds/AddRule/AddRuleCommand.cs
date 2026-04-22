using MediatR;
using Venue_System.Application.Bases;

namespace Venue_System.Application.Features.Venues.Commnds.AddRule
{

    public record AddRuleCommand : IRequest<Response<Guid>>
    {
        public Guid VenueId { get; init; }
        public string RuleText { get; init; }
        public bool IsMandatory { get; init; }

    }
}
