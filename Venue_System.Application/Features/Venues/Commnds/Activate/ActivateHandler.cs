using MediatR;
using Venue_System.Application.Bases;
using Venue_System.Application.Interfaces.Repositories;
using Venue_System.Application.Interfaces.Services;

namespace Venue_System.Application.Features.Venues.Commnds.Activate
{
    public class ActivateHandler : ResponseHandler, IRequestHandler<ActivateCommand, Response<string>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICurrentUserService _currentUserService;

        public ActivateHandler(
            IUnitOfWork unitOfWork,
            ICurrentUserService currentUserService)
        {
            _unitOfWork = unitOfWork;
            _currentUserService = currentUserService;
        }

        public async Task<Response<string>> Handle(ActivateCommand request, CancellationToken cancellationToken)
        {
            var venue = await _unitOfWork.Venue.GetByIdAsync(request.VenueId, cancellationToken);
            var userId = _currentUserService.UserId;
            var role = _currentUserService.Role;
            if (venue == null)
                return NotFound<string>("Venue is not found");

            if (userId != venue.OwnerId && role != "Admin")
            {
                return Forbidden<string>("Access denied");
            }
            venue.Activate();
            await _unitOfWork.CompleteAsync(cancellationToken);
            return Success<string>(venue.Id.ToString(), "The Venue Activated Successfully");
        }
    }
}
