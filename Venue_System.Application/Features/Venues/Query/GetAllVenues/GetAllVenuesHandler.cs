using AutoMapper;
using MediatR;
using Venue_System.Application.Bases;
using Venue_System.Application.Features.Venues.DTO;
using Venue_System.Application.Interfaces.Repositories;

namespace Venue_System.Application.Features.Venues.Query.GetAllVenues
{
    public class GetAllVenuesHandler : ResponseHandler, IRequestHandler<GetAllVenuesQuery, Response<List<VenueDTo>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllVenuesHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Response<List<VenueDTo>>> Handle(GetAllVenuesQuery request, CancellationToken cancellationToken)
        {
            var venues = await _unitOfWork.Venue.GetActiveDtosAsync(_mapper);


            return new Response<List<VenueDTo>>(venues)
            {
                Message = "Venues retrieved successfully",
            };

        }
    }
}
