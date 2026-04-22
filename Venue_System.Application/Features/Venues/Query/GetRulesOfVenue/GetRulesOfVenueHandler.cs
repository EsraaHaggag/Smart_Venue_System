using AutoMapper;
using MediatR;
using Venue_System.Application.Bases;
using Venue_System.Application.Comman.DTO;
using Venue_System.Application.Features.DTO;
using Venue_System.Application.Interfaces.Repositories;

namespace Venue_System.Application.Features.Venues.Query.GetRulesOfVenue
{
    public class GetRulesOfVenueHandler : ResponseHandler, IRequestHandler<GetRulesOfVenueQuery, Response<List<VenueRuleDTo>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetRulesOfVenueHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Response<List<VenueRuleDTo>>> Handle(GetRulesOfVenueQuery request, CancellationToken cancellationToken)
        {
            var venue = await _unitOfWork.Venue.GetByIdWithRulesAsync(request.VenueId, cancellationToken);

            if (venue == null)
                return NotFound<List<VenueRuleDTo>>("Venue is not found");
            var rules = _mapper.Map<List<VenueRuleDTo>>(venue.Rules);

            if (rules == null || !rules.Any())
                return NotFound<List<VenueRuleDTo>>("No rules found for this venue");
            return new Response<List<VenueRuleDTo>>(rules)
            {
                Message = "Venues retrieved successfully",
            };
        }
    }
}
