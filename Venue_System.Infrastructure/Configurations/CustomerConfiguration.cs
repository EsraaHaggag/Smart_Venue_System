using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Venue_System.Domain.Entities;
using Venue_System.Infrastructure.Identity;

namespace Venue_System.Infrastructure.Configurations
{
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.HasOne<ApplicationUser>()
                   .WithOne()
                   .HasForeignKey<Customer>(c => c.Id);
        }
    }
}
