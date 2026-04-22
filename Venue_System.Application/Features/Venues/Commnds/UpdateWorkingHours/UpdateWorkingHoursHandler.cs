using MediatR;
using Venue_System.Application.Bases;
using Venue_System.Application.Interfaces.Repositories;

namespace Venue_System.Application.Features.Venues.Commnds.UpdateWorkingHours
{
    public class UpdateWorkingHoursHandler : ResponseHandler, IRequestHandler<UpdateWorkingHoursCommand, Response<string>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateWorkingHoursHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Response<string>> Handle(UpdateWorkingHoursCommand request, CancellationToken cancellationToken)
        {
            var venue = await _unitOfWork.Venue.GetByIdWithWorkingHourAsync(request.VenueId, cancellationToken);

            if (venue == null)
                return NotFound<string>("Venue is not found");

            venue.UpdateWorkingHours(request.Day, request.OpenFrom, request.OpenTo, request.IsClosed);
            await _unitOfWork.CompleteAsync(cancellationToken);
            return Success<string>("The WorkingHour Updated Successfully");
        }
    }
}
