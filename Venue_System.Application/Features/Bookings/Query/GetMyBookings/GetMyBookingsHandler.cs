using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Venue_System.Application.Bases;
using Venue_System.Application.Comman.Extensions;
using Venue_System.Application.Comman.Wrappers;
using Venue_System.Application.Features.Bookings.DTO;
using Venue_System.Application.Interfaces.Repositories;
using Venue_System.Application.Interfaces.Services;

namespace Venue_System.Application.Features.Bookings.Query.GetMyBookings
{
    internal class GetMyBookingsHandler : ResponseHandler, IRequestHandler<GetMyBookingsQuery, Response<PaginatedResult<BookingDTo>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICurrentUserService _currentUserService;
        private readonly IMapper _mapper;

        public GetMyBookingsHandler(
            IUnitOfWork unitOfWork,
            ICurrentUserService currentUserService,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _currentUserService = currentUserService;
            _mapper = mapper;
        }
        public async Task<Response<PaginatedResult<BookingDTo>>> Handle(GetMyBookingsQuery request, CancellationToken cancellationToken)
        {
            var userId = _currentUserService.UserId;

            var query = _unitOfWork.Booking
                .GetQueryable()
                .Where(b => b.CustomerId == userId);


            if (request.Status.HasValue)
            {
                query = query.Where(b => b.Status == request.Status.Value);
            }

            var result = await query
                .ProjectTo<BookingDTo>(_mapper.ConfigurationProvider)
                .ToPaginatedListAsync(request.PageNumber, request.PageSize);


            return Success<PaginatedResult<BookingDTo>>(result, "Venues retrieved successfully");
        }


    }
}
