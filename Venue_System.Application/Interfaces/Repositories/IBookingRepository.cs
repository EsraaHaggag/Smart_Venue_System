using Venue_System.Domain.Entities;

namespace Venue_System.Application.Interfaces.Repositories
{
    public interface IBookingRepository
        : IGenericRepositoryAsync<Booking>
    {
        Task<List<Booking>> GetBookingByVenueIdAsync(Guid VenueId, CancellationToken cancellationToken);
        Task<Booking?> GetBookingWithVenueAsync(Guid BookingId, CancellationToken cancellationToken);
        IQueryable<Booking> GetQueryable();
    }
}
