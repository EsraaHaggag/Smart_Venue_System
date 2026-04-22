using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Venue_System.Domain.Entities;
using Venue_System.Infrastructure.Identity;

namespace Venue_System.Infrastructure.Configurations
{
    public class VenueOwnerConfiguration : IEntityTypeConfiguration<VenueOwner>
    {
        public void Configure(EntityTypeBuilder<VenueOwner> builder)
        {
            builder.HasOne<ApplicationUser>()
                   .WithOne()
                   .HasForeignKey<VenueOwner>(vo => vo.Id);
        }
    }
}
