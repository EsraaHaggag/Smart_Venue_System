using Venue_System.Application.Comman.DTO;

namespace Venue_System.Application.Features.Bookings.DTO
{
    public class BookingDTo
    {
        public Guid Id { get; set; }
        public Guid VenueId { get; set; }
        public string VenueName { get; set; }

        public Guid CustomerId { get; set; }

        public DateTime Start { get; set; }
        public DateTime End { get; set; }

        public string Status { get; set; }
        public MoneyDTo TotalPrice { get; set; }
    }
}
