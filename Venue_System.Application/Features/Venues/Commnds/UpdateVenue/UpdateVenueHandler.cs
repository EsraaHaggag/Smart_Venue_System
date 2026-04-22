using MediatR;
using Venue_System.Application.Bases;
using Venue_System.Application.Interfaces.Repositories;
using Venue_System.Application.Interfaces.Services;
using Venue_System.Domain.ValueObjects;

namespace Venue_System.Application.Features.Venues.Commnds.UpdateVenue
{
    public class UpdateVenueHandler : ResponseHandler, IRequestHandler<UpdateVenueCommand, Response<string>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICurrentUserService _currentUserService;

        public UpdateVenueHandler(
            IUnitOfWork unitOfWork,
            ICurrentUserService currentUserService)
        {
            _unitOfWork = unitOfWork;
            _currentUserService = currentUserService;
        }
        public async Task<Response<string>> Handle(UpdateVenueCommand request, CancellationToken cancellationToken)
        {
            var venue = await _unitOfWork.Venue.GetByIdAsync(request.Id, cancellationToken);

            var userId = _currentUserService.UserId;
            if (venue == null)
                return NotFound<string>("Venue is not found");

            if (userId != venue.OwnerId)
            {
                return Forbidden<string>("Access denied");
            }

            var money = new Money(request.Amount, request.Currency);

            var location = new VenueLocation(
                request.Country,
                request.City,
                request.Address,
                request.GoogleMapUrl);

            venue.Update(request.Name, request.Description, location, request.Capacity, money);

            await _unitOfWork.CompleteAsync(cancellationToken);
            return Success<string>(venue.Id.ToString(), "The Info Updated Successfully");

        }
    }
}
