using Microsoft.EntityFrameworkCore;
using Venue_System.Application.Interfaces.Repositories;
using Venue_System.Domain.Entities;
using Venue_System.Domain.Enums;
using Venue_System.Infrustrucure.DBContext;

namespace Venue_System.Infrastructure.Repositories
{
    public class BookingRepository : GenericRepositoryAsync<Booking>, IBookingRepository
    {
        private readonly DbSet<Booking> _Booking;

        public BookingRepository(ApplictionDBContext dBContext) : base(dBContext)
        {
            _Booking = dBContext.Set<Booking>();
        }
        public async Task<List<Booking>> GetBookingByVenueIdAsync(Guid VenueId, CancellationToken cancellationToken)
        {
            return await _Booking
                .Where(v => v.VenueId == VenueId && v.IsDeleted == false && v.Status != BookingStatus.Cancelled)
                .ToListAsync();
        }

        public async Task<Booking?> GetBookingWithVenueAsync(Guid BookingId, CancellationToken cancellationToken)
        {
            return await _Booking
                .Where(v => v.Id == BookingId)
                .Include(v => v.Venue)
                .FirstOrDefaultAsync();
        }

        public IQueryable<Booking> GetQueryable()
        {
            return _Booking.AsQueryable();
        }
    }
}
