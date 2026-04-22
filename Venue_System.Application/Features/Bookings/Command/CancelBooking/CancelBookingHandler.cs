using MediatR;
using Venue_System.Application.Bases;
using Venue_System.Application.Interfaces.Repositories;
using Venue_System.Application.Interfaces.Services;

namespace Venue_System.Application.Features.Bookings.Command.CancelBooking
{
    public class CancelBookingHandler : ResponseHandler,
    IRequestHandler<CancelBookingCommand, Response<Guid>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IVenueNotifierService _notifier;
        private readonly ICurrentUserService _currentUserService;

        public CancelBookingHandler(IUnitOfWork unitOfWork, IVenueNotifierService notifier, ICurrentUserService currentUserService)
        {
            _unitOfWork = unitOfWork;
            _notifier = notifier;
            _currentUserService = currentUserService;
        }

        public async Task<Response<Guid>> Handle(
            CancelBookingCommand request,
            CancellationToken cancellationToken)
        {
            var booking = await _unitOfWork.Booking.GetBookingWithVenueAsync(request.BookingId, cancellationToken);

            if (booking == null)
                return NotFound<Guid>("Booking not found");
            var userId = _currentUserService.UserId;

            if (userId != booking.CustomerId)
            {
                return Forbidden<Guid>("Access denied");
            }
            var policy = booking.Venue.CancellationPolicy;
            var cancellation = booking.Cancel(policy, DateTime.UtcNow);


            await _unitOfWork.BookingCancellation.AddAsync(cancellation);

            await _unitOfWork.CompleteAsync(cancellationToken);

            await _notifier.NotifyBookingCancelled(
                booking.VenueId,
                booking.Start,
                booking.End
            );
            return Success(cancellation.Id, "The Booking Canceled Successfully");
        }
    }
}
