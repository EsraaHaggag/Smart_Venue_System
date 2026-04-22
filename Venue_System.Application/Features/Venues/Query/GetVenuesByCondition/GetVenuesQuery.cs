using MediatR;
using Venue_System.Application.Comman.Wrappers;
using Venue_System.Application.Features.Venues.DTO;

namespace Venue_System.Application.Features.Venues.Query.GetVenuesByCondition
{
    public record GetVenuesQuery : IRequest<PaginatedResult<VenueDTo>>
    {
        public decimal? MinPrice { get; init; }
        public decimal? MaxPrice { get; init; }
        public int? Capacity { get; init; }

        public string? SortBy { get; init; }
        public string? SortDirection { get; init; }

        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
