using MediatR;
using Venue_System.Application.Bases;
using Venue_System.Application.Interfaces.Repositories;
using Venue_System.Application.Interfaces.Services;
using Venue_System.Domain.Entities;

namespace Venue_System.Application.Features.Bookings.Command.CreateBooking
{
    public class CreateBookingHandler : ResponseHandler, IRequestHandler<CreateBookingCommand, Response<Guid>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IVenueNotifierService _notifier;
        private readonly ICurrentUserService _currentUserService;
        public CreateBookingHandler(IUnitOfWork unitOfWork, IVenueNotifierService notifier, ICurrentUserService currentUserService)
        {
            _unitOfWork = unitOfWork;
            _notifier = notifier;
            _currentUserService = currentUserService;
        }


        public async Task<Response<Guid>> Handle(CreateBookingCommand request, CancellationToken cancellationToken)
        {
            await using var transaction = await _unitOfWork.BeginTransactionAsync(cancellationToken);
            var customerId = _currentUserService.UserId;
            try
            {
                var venue = await _unitOfWork.Venue.GetByIdAsync(request.VenueId, cancellationToken);

                if (venue == null)
                    return NotFound<Guid>("Venue is not found");

                var bookings = await _unitOfWork.Booking
                    .GetBookingByVenueIdAsync(request.VenueId, cancellationToken);

                var isAvailable = venue.IsAvailable(request.Start, request.End, bookings);

                if (!isAvailable)
                {
                    await transaction.RollbackAsync(cancellationToken);
                    return BadRequest<Guid>("This time slot is not available");
                }

                var newBooking = Booking.Create(
                    request.VenueId,
                    customerId,
                    request.Start,
                    request.End,
                    venue.BaseHourlyPrice.Amount,
                    venue.BaseHourlyPrice.Currency
                );

                await _unitOfWork.Booking.AddAsync(newBooking);
                await _unitOfWork.CompleteAsync(cancellationToken);

                await transaction.CommitAsync(cancellationToken);

                await _notifier.NotifyBooked(request.VenueId, request.Start, request.End);

                return Success(newBooking.Id, "The Booking Created Successfully");
            }
            catch (Exception)
            {
                await transaction.RollbackAsync(cancellationToken);
                throw;
            }
        }
    }
}
