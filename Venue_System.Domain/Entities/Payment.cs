using Venue_System.Domain.Enums;
using Venue_System.Domain.ValueObjects;

namespace Venue_System.Domain.Entities
{
    public class Payment
    {
        public Guid Id { get; set; }
        public Guid BookingId { get; set; }
        public Money Amount { get; set; }
        public PaymentStatus Status { get; set; }
        public DateTime PaidAt { get; set; }
    }

}
