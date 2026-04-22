using MediatR;
using Venue_System.Application.Bases;
using Venue_System.Domain.Enums;

namespace Venue_System.Application.Features.Venues.Commnds.UpdateVenue
{
    public record UpdateVenueCommand : IRequest<Response<string>>
    {
        public Guid Id { get; init; }
        public string Name { get; init; }
        public string Description { get; init; }
        public string Country { get; init; }
        public string City { get; init; }
        public string Address { get; init; }
        public string GoogleMapUrl { get; init; }
        public int Capacity { get; init; }
        public decimal Amount { get; init; }
        public Currency Currency { get; init; }

    }
}
