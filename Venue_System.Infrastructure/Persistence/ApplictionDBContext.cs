using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Venue_System.Application.Interfaces.Services;
using Venue_System.Domain.Entities;
using Venue_System.Infrastructure.Identity;


namespace Venue_System.Infrustrucure.DBContext
{
    public class ApplictionDBContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid, IdentityUserClaim<Guid>, IdentityUserRole<Guid>, IdentityUserLogin<Guid>, IdentityRoleClaim<Guid>, IdentityUserToken<Guid>>, IApplicationDbContext
    {
        public ApplictionDBContext()
        {
        }

        public ApplictionDBContext(DbContextOptions<ApplictionDBContext> options) : base(options)
        {

        }
        public DbSet<VenueOwner> VenueOwners { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers
        {
            get; set;
        }
        public DbSet<AuditLog> AuditLogs { get; set; }
        public DbSet<VenueRule> VenueRules { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Venue> Venues { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplictionDBContext).Assembly);
        }


    }
}
