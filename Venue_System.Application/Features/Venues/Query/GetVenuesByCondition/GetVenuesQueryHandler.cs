using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Venue_System.Application.Comman.Extensions;
using Venue_System.Application.Comman.Wrappers;
using Venue_System.Application.Features.Venues.DTO;
using Venue_System.Application.Interfaces.Repositories;

namespace Venue_System.Application.Features.Venues.Query.GetVenuesByCondition
{
    public class GetVenuesQueryHandler : IRequestHandler<GetVenuesQuery, PaginatedResult<VenueDTo>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetVenuesQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<PaginatedResult<VenueDTo>> Handle(GetVenuesQuery request, CancellationToken cancellationToken)
        {
            var query = _unitOfWork.Venue.GetQueryable();

            // 🟢 Filtering

            if (request.MinPrice.HasValue)
                query = query.Where(v => v.BaseHourlyPrice.Amount >= request.MinPrice.Value);

            if (request.MaxPrice.HasValue)
                query = query.Where(v => v.BaseHourlyPrice.Amount <= request.MaxPrice.Value);

            if (request.Capacity.HasValue)
                query = query.Where(v => v.Capacity >= request.Capacity.Value);

            // 🔵 Sorting

            if (!string.IsNullOrEmpty(request.SortBy))
            {
                switch (request.SortBy.ToLower())
                {
                    case "price":
                        query = request.SortDirection?.ToLower() == "desc"
                            ? query.OrderByDescending(v => v.BaseHourlyPrice.Amount)
                            : query.OrderBy(v => v.BaseHourlyPrice.Amount);
                        break;

                    case "capacity":
                        query = request.SortDirection?.ToLower() == "desc"
                            ? query.OrderByDescending(v => v.Capacity)
                            : query.OrderBy(v => v.Capacity);
                        break;

                    default:
                        query = query.OrderBy(v => v.Id);
                        break;
                }
            }
            else
            {
                query = query.OrderBy(v => v.Id);
            }

            // 🟣 Pagination
            var result = await query
                .ProjectTo<VenueDTo>(_mapper.ConfigurationProvider)
                .ToPaginatedListAsync(request.PageNumber, request.PageSize);
            return result;
        }
    }
}
