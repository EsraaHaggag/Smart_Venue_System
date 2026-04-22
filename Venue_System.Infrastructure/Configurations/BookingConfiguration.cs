using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Venue_System.Domain.Entities;

namespace Venue_System.Infrastructure.Configurations
{
    public class BookingConfiguration : IEntityTypeConfiguration<Booking>
    {
        public void Configure(EntityTypeBuilder<Booking> builder)
        {
            builder.HasKey(b => b.Id);

            builder.OwnsOne(b => b.TotalPrice, money =>
            {
                money.Property(m => m.Amount).IsRequired();
                money.Property(m => m.Currency).HasConversion<string>().IsRequired();
            });

            builder.HasOne(b => b.Venue)
                   .WithMany()
                   .HasForeignKey(b => b.VenueId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(b => new { b.VenueId, b.Start, b.End });
        }
    }
}
