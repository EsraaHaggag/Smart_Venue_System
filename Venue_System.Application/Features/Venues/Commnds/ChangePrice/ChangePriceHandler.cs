using MediatR;
using Venue_System.Application.Bases;
using Venue_System.Application.Interfaces.Repositories;
using Venue_System.Application.Interfaces.Services;
using Venue_System.Domain.ValueObjects;

namespace Venue_System.Application.Features.Venues.Commnds.ChangePrice
{
    public class ChangePriceHandler : ResponseHandler, IRequestHandler<ChangePriceCommand, Response<string>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICurrentUserService _currentUserService;

        public ChangePriceHandler(
            IUnitOfWork unitOfWork,
            ICurrentUserService currentUserService)
        {
            _unitOfWork = unitOfWork;
            _currentUserService = currentUserService;
        }


        public async Task<Response<string>> Handle(ChangePriceCommand request, CancellationToken cancellationToken)
        {
            var venue = await _unitOfWork.Venue.GetByIdAsync(request.VenueId, cancellationToken);

            var userId = _currentUserService.UserId;

            if (venue == null)
                return NotFound<string>("Venue is not found");

            if (userId != venue.OwnerId)
            {
                return Forbidden<string>("Access denied");
            }

            var money = new Money(request.amount, request.currency);

            venue.ChangePrice(money);
            await _unitOfWork.CompleteAsync(cancellationToken);
            return Success<string>(venue.Id.ToString(), "The Price Updated Successfully");
        }
    }
}
