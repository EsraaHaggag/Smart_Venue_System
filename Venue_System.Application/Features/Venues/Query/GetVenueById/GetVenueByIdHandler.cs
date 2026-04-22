using AutoMapper;
using MediatR;
using Venue_System.Application.Bases;
using Venue_System.Application.Features.Venues.DTO;
using Venue_System.Application.Interfaces.Repositories;

namespace Venue_System.Application.Features.Venues.Query.GetVenueById
{
    public class GetVenueByIdHandler : ResponseHandler, IRequestHandler<GetVenueByIdQuery, Response<VenueDTo>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetVenueByIdHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Response<VenueDTo>> Handle(GetVenueByIdQuery request, CancellationToken cancellationToken)
        {
            var venue = await _unitOfWork.Venue.GetActiveDtosByIdAsync(_mapper, request.VenueId);
            if (venue == null)
                return NotFound<VenueDTo>("Venue is not found");

            return Success<VenueDTo>(venue, "Venues retrieved successfully");
        }
    }
}
