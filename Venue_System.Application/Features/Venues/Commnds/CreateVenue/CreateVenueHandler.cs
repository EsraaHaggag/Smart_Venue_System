using MediatR;
using Venue_System.Application.Bases;
using Venue_System.Application.Interfaces.Repositories;
using Venue_System.Application.Interfaces.Services;
using Venue_System.Domain.ValueObjects;

namespace Venue_System.Application.Features.Venues.Commnds.CreateVenue
{

    public class CreateVenueHandler
      : ResponseHandler, IRequestHandler<CreateVenueCommand, Response<Guid>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICurrentUserService _currentUserService;

        public CreateVenueHandler(
            IUnitOfWork unitOfWork,
            ICurrentUserService currentUserService)
        {
            _unitOfWork = unitOfWork;
            _currentUserService = currentUserService;
        }

        public async Task<Response<Guid>> Handle(
       CreateVenueCommand request,
       CancellationToken cancellationToken)
        {
            var ownerId = _currentUserService.UserId;


            var money = new Money(request.Amount, request.Currency);

            var location = new VenueLocation(
                request.Country,
                request.City,
                request.Address,
                request.GoogleMapUrl
            );


            var venue = Venue.Register(
                request.Name,
                request.Description,
                location,
                request.Capacity,
                money,
                ownerId
            );

            if (request.Rules != null && request.Rules.Any())
            {
                foreach (var rule in request.Rules)
                {
                    venue.AddRule(rule.RuleText, rule.IsMandatory);
                }
            }


            await _unitOfWork.Venue.AddAsync(venue);
            await _unitOfWork.CompleteAsync(cancellationToken);


            return Success<Guid>(venue.Id, "Venue Created Succssefuly");
        }
    }

}

