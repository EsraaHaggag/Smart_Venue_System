using MediatR;
using Venue_System.Application.Bases;
using Venue_System.Domain.Enums;

namespace Venue_System.Application.Features.Venues.Commnds.ChangePrice
{
    public record ChangePriceCommand : IRequest<Response<string>>
    {
        public Guid VenueId { get; init; }
        public decimal amount { get; init; }
        public Currency currency { get; init; }

    }
}
