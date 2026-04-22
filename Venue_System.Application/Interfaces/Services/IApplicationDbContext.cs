using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Venue_System.Domain.Entities;

namespace Venue_System.Application.Interfaces.Services
{
    public interface IApplicationDbContext
    {
        DatabaseFacade Database { get; }
        DbSet<Customer> Customers { get; }
        DbSet<VenueOwner> VenueOwners { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
