using MediatR;
using Venue_System.Application.Bases;
using Venue_System.Application.Interfaces.Repositories;
using Venue_System.Application.Interfaces.Services;

namespace Venue_System.Application.Features.Venues.Commnds.RemoveRule
{
    public class RemoveRuleHandler : ResponseHandler, IRequestHandler<RemoveRuleCommand, Response<string>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICurrentUserService _currentUserService;

        public RemoveRuleHandler(
            IUnitOfWork unitOfWork,
            ICurrentUserService currentUserService)
        {
            _unitOfWork = unitOfWork;
            _currentUserService = currentUserService;
        }

        public async Task<Response<string>> Handle(RemoveRuleCommand request, CancellationToken cancellationToken)
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

            venue.RemoveRule(request.RuleId);
            await _unitOfWork.CompleteAsync(cancellationToken);
            return Success<string>("The Rule Deleted Successfully");

        }
    }
}
