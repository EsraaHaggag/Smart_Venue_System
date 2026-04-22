using MediatR;
using Venue_System.Application.Bases;
using Venue_System.Application.Interfaces.Repositories;
using Venue_System.Application.Interfaces.Services;

namespace Venue_System.Application.Features.Venues.Commnds.SetCancellationPolicy
{
    public class UpdateCancellationPolicyHandler : ResponseHandler, IRequestHandler<SetCancellationPolicyCommand, Response<string>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICurrentUserService _currentUserService;

        public UpdateCancellationPolicyHandler(
            IUnitOfWork unitOfWork,
            ICurrentUserService currentUserService)
        {
            _unitOfWork = unitOfWork;
            _currentUserService = currentUserService;
        }

        public async Task<Response<string>> Handle(SetCancellationPolicyCommand request, CancellationToken cancellationToken)
        {
            var venue = await _unitOfWork.Venue.GetByIdAsync(request.VenueId, cancellationToken);

            var ownerId = _currentUserService.UserId;

            if (venue == null)
                return NotFound<string>("Venue is not found");

            if (ownerId != venue.OwnerId)
                return NotFound<string>("Owner Only Can Set Policy");
            var policy = venue.SetCancellationPolicy(request.AllowedHoursBeforeEvent, request.RefundPercentage);
            await _unitOfWork.CancellationPolicy.AddAsync(policy);
            await _unitOfWork.CompleteAsync(cancellationToken);
            return Success<string>("The Cancellation Policy Updated Successfully");

        }
    }
}
