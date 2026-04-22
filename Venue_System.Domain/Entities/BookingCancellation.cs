using Venue_System.Domain.ValueObjects;

namespace Venue_System.Domain.Entities
{
    public class BookingCancellation
    {
        public Guid Id { get; set; }
        public Guid BookingId { get; set; }
        public DateTime CancelledAt { get; set; }
        public Money RefundedAmount { get; set; }
    }

}
