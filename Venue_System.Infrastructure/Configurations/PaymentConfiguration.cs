using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Venue_System.Domain.Entities;

namespace Venue_System.Infrastructure.Configurations
{
    public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
    {
        public void Configure(EntityTypeBuilder<Payment> builder)
        {
            builder.HasKey(p => p.Id);

            builder.OwnsOne(p => p.Amount, a =>
            {
                a.Property(m => m.Amount)
                 .HasColumnName("Amount");

                a.Property(m => m.Currency)
                 .HasColumnName("Currency")
                 .HasConversion<string>();
            });
        }
    }
}
