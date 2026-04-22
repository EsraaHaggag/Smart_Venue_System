using Microsoft.EntityFrameworkCore;
using Venue_System.Application.Interfaces.Repositories;
using Venue_System.Domain.Entities;
using Venue_System.Infrustrucure.DBContext;

namespace Venue_System.Infrastructure.Repositories
{
    public class BookingCancellationRepository : GenericRepositoryAsync<BookingCancellation>, IBookingCancellationRepository
    {
        private readonly DbSet<BookingCancellation> _BookingCancellationRepository;

        public BookingCancellationRepository(ApplictionDBContext dBContext) : base(dBContext)
        {
            _BookingCancellationRepository = dBContext.Set<BookingCancellation>();


        }

    }
}
