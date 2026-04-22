using MediatR;
using Venue_System.Application.Bases;
using Venue_System.Application.Interfaces.Repositories;
using Venue_System.Application.Interfaces.Services;

namespace Venue_System.Application.Features.Venues.Commnds.RemoveVenue
{
    public class RemoveVenueHandler : ResponseHandler, IRequestHandler<RemoveVenueCommand, Response<string>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICurrentUserService _currentUserService;

        public RemoveVenueHandler(
            IUnitOfWork unitOfWork,
            ICurrentUserService currentUserService)
        {
            _unitOfWork = unitOfWork;
            _currentUserService = currentUserService;
        }

        public async Task<Response<string>> Handle(RemoveVenueCommand request, CancellationToken cancellationToken)
        {
            var userId = _currentUserService.UserId;
            var role = _currentUserService.Role;
            var venue = await _unitOfWork.Venue.GetByIdAsync(request.VenueId, cancellationToken);

            if (venue == null)
                return NotFound<string>("Venue is not found");

            if (userId != venue.OwnerId && role != "Admin")
            {
                return Unauthorized<string>("Only owner or admin can perform this action");
            }
            venue.SoftDelete();
            await _unitOfWork.CompleteAsync(cancellationToken);
            return Success<string>(venue.Id.ToString(), "The Venue Deleted Successfully");
        }
    }
}
