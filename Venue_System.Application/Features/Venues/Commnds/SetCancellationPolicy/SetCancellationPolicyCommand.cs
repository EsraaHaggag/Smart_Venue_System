using MediatR;
using Venue_System.Application.Bases;

namespace Venue_System.Application.Features.Venues.Commnds.SetCancellationPolicy
{
    public record SetCancellationPolicyCommand : IRequest<Response<string>>
    {
        public Guid VenueId { get; init; }
        public int AllowedHoursBeforeEvent { get; set; }
        public decimal RefundPercentage { get; set; }

    }
}
