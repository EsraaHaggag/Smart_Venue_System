using MediatR;
using Venue_System.Application.Bases;

namespace Venue_System.Application.Features.Venues.Commnds.AddWorkingHours
{
    public record AddWorkingHoursCommand : IRequest<Response<string>>
    {
        public Guid VenueId { get; init; }
        public DayOfWeek Day { get; init; }
        public TimeSpan OpenFrom { get; init; }
        public TimeSpan OpenTo { get; init; }
        public bool IsClosed { get; init; }

    }

}

