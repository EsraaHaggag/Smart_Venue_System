using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Data;
using Venue_System.Application.Interfaces.Repositories;
using Venue_System.Infrustrucure.DBContext;
namespace Venue_System.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplictionDBContext _context;

        public IVenueRepository Venue { get; }
        public IBookingRepository Booking { get; }
        public IBookingCancellationRepository BookingCancellation { get; }
        public ICancellationPolicyRepository CancellationPolicy { get; }

        public UnitOfWork(ApplictionDBContext context)
        {
            _context = context;
            Venue = new VenueRepository(_context);
            Booking = new BookingRepository(_context);
            BookingCancellation = new BookingCancellationRepository(_context);
            CancellationPolicy
                = new CancellationPolicyRepository(_context);
        }

        public async Task<int> CompleteAsync(CancellationToken cancellationToken)
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }



        public async Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken)
        {
            return await _context.Database
                .BeginTransactionAsync(IsolationLevel.Serializable, cancellationToken);
        }
        public IDbContextTransaction BeginTransaction()
        {
            return _context.Database.BeginTransaction();
        }

        public void Commit()
        {
            _context.Database.CommitTransaction();
        }

        public void RollBack()
        {
            _context.Database.RollbackTransaction();
        }
    }
}
