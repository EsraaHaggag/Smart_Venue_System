using MediatR;
using Venue_System.Application.Bases;
using Venue_System.Application.Interfaces.Repositories;

namespace Venue_System.Application.Features.Venues.Commnds.AddWorkingHours
{
    public class AddWorkingHoursHandler : ResponseHandler, IRequestHandler<AddWorkingHoursCommand, Response<string>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public AddWorkingHoursHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Response<string>> Handle(AddWorkingHoursCommand request, CancellationToken cancellationToken)
        {
            var venue = await _unitOfWork.Venue.GetByIdWithWorkingHourAsync(request.VenueId, cancellationToken);

            if (venue == null)
                return NotFound<string>("Venue is not found");

            venue.AddWorkingHours(request.Day, request.OpenFrom, request.OpenTo, request.IsClosed);
            await _unitOfWork.CompleteAsync(cancellationToken);
            return Success<string>("The WorkingHour Added Successfully");
        }
    }
}
