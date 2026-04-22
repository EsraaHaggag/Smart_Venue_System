using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Venue_System.Infrastructure.Configurations
{
    public class VenueConfiguration : IEntityTypeConfiguration<Venue>
    {
        public void Configure(EntityTypeBuilder<Venue> builder)
        {
            builder.HasKey(v => v.Id);


            builder.OwnsOne(v => v.Location);
            builder.OwnsOne(v => v.BaseHourlyPrice);

            builder.OwnsMany(v => v.WorkingHours, wh =>
            {
                wh.WithOwner().HasForeignKey("VenueId");
                wh.ToTable("WorkingHours");
                wh.HasKey("VenueId", "Day");
            });


            builder.HasMany(v => v.Rules)
                   .WithOne(r => r.Venue)
                   .HasForeignKey(r => r.VenueId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(v => v.CancellationPolicy)
                   .WithMany()
                   .HasForeignKey(v => v.CancellationPolicyId)
                   .OnDelete(DeleteBehavior.SetNull);

            builder.Navigation(v => v.Rules).UsePropertyAccessMode(PropertyAccessMode.Field);
            builder.Navigation(v => v.WorkingHours).UsePropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}
