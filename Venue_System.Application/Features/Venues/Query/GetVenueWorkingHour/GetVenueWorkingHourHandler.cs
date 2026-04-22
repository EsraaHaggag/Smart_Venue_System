using AutoMapper;
using MediatR;
using Venue_System.Application.Bases;
using Venue_System.Application.Comman.DTO;
using Venue_System.Application.Features.DTO;
using Venue_System.Application.Interfaces.Repositories;

namespace Venue_System.Application.Features.Venues.Query.GetVenueWorkingHour
{
    public class GetVenueWorkingHourHandler : ResponseHandler, IRequestHandler<GetVenueWorkingHourQuery, Response<List<WorkingHourDTo>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetVenueWorkingHourHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Response<List<WorkingHourDTo>>> Handle(GetVenueWorkingHourQuery request, CancellationToken cancellationToken)
        {
            var venue = await _unitOfWork.Venue.GetActiveDtosByIdAsync(_mapper, request.VenueId);

            if (venue == null)
                return NotFound<List<WorkingHourDTo>>("Venue is not found");

            return new Response<List<WorkingHourDTo>>(venue.WorkingHours)
            {
                Message = "Venues retrieved successfully",
            };
        }
    }
}
