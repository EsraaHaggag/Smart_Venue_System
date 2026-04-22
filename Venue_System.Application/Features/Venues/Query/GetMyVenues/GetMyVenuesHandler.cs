using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Venue_System.Application.Bases;
using Venue_System.Application.Features.Venues.DTO;
using Venue_System.Application.Interfaces.Repositories;
using Venue_System.Application.Interfaces.Services;

namespace Venue_System.Application.Features.Venues.Query.GetMyVenues
{
    public class GetMyVenuesHandler : ResponseHandler, IRequestHandler<GetMyVenuesQuery, Response<List<VenueDTo>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICurrentUserService _currentUserService;
        private readonly IMapper _mapper;

        public GetMyVenuesHandler(
            IUnitOfWork unitOfWork,
            ICurrentUserService currentUserService,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _currentUserService = currentUserService;
            _mapper = mapper;
        }

        public async Task<Response<List<VenueDTo>>> Handle(GetMyVenuesQuery request, CancellationToken cancellationToken)
        {
            var ownerId = _currentUserService.UserId;

            var query = _unitOfWork.Venue
                .GetQueryable()
                .Where(v => v.OwnerId == ownerId);

            var venues = await query
                .ProjectTo<VenueDTo>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return Success<List<VenueDTo>>(venues);
        }
    }
}
