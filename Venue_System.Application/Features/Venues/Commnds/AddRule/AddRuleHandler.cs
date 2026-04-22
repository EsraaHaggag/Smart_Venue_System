using MediatR;
using Venue_System.Application.Bases;
using Venue_System.Application.Interfaces.Repositories;
using Venue_System.Application.Interfaces.Services;

namespace Venue_System.Application.Features.Venues.Commnds.AddRule
{
    public class AddRuleHandler : ResponseHandler,
        IRequestHandler<AddRuleCommand, Response<Guid>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICurrentUserService _currentUserService;

        public AddRuleHandler(
            IUnitOfWork unitOfWork,
            ICurrentUserService currentUserService)
        {
            _unitOfWork = unitOfWork;
            _currentUserService = currentUserService;
        }
        public async Task<Response<Guid>> Handle(AddRuleCommand request, CancellationToken cancellationToken)
        {
            var venue = await _unitOfWork.Venue
                .GetByIdWithRulesAsync(request.VenueId, cancellationToken);


            var userId = _currentUserService.UserId;
            var role = _currentUserService.Role;
            if (venue == null)
                return NotFound<Guid>("Venue is not found");

            if (userId != venue.OwnerId && role != "Admin")
            {
                return Forbidden<Guid>("Access denied");
            }
            Guid Id = venue.AddRule(request.RuleText, request.IsMandatory);

            await _unitOfWork.CompleteAsync(cancellationToken);
            return Success<Guid>(Id, "The Rule Added Successfully");
        }
    }
}
