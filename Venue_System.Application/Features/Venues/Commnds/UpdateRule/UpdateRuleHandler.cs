using MediatR;
using Venue_System.Application.Bases;
using Venue_System.Application.Interfaces.Repositories;
using Venue_System.Application.Interfaces.Services;

namespace Venue_System.Application.Features.Venues.Commnds.UpdateRule
{
    public class UpdateRuleHandler : ResponseHandler, IRequestHandler<UpdateRuleCommand, Response<string>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICurrentUserService _currentUserService;

        public UpdateRuleHandler(
            IUnitOfWork unitOfWork,
            ICurrentUserService currentUserService)
        {
            _unitOfWork = unitOfWork;
            _currentUserService = currentUserService;
        }


        public async Task<Response<string>> Handle(UpdateRuleCommand request, CancellationToken cancellationToken)
        {
            var venue = await _unitOfWork.Venue
                      .GetByIdWithRulesAsync(request.VenueId, cancellationToken);
            var userId = _currentUserService.UserId;
            if (venue == null)
                return NotFound<string>("Venue is not found");

            if (userId != venue.OwnerId)
            {
                return Forbidden<string>("Access denied");
            }

            venue.UpdateRule(request.RuleId, request.RuleText, request.IsMandatory);
            await _unitOfWork.CompleteAsync(cancellationToken);
            return Success<string>("The Rule Updated Successfully");
        }
    }
}
