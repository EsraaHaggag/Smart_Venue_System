namespace Venue_System.Application.Interfaces.Repositories
{
    using Microsoft.EntityFrameworkCore.Storage;

    public interface IUnitOfWork
    {
        IVenueRepository Venue { get; }
        IBookingRepository Booking { get; }
        IBookingCancellationRepository BookingCancellation { get; }
        ICancellationPolicyRepository CancellationPolicy { get; }

        Task<int> CompleteAsync(CancellationToken cancellationToken);

        Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken);
        IDbContextTransaction BeginTransaction();
        void Commit();
        void RollBack();
    }
}
