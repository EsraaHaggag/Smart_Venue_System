using Venue_System.Domain.Domain.Entities;
using Venue_System.Domain.Enums;
using Venue_System.Domain.ValueObjects;

namespace Venue_System.Domain.Entities
{
    public class Booking : CommonData
    {
        public Guid Id { get; private set; }
        public Guid VenueId { get; private set; }
        public Venue Venue { get; private set; }
        public Guid CustomerId { get; private set; }

        public DateTime Start { get; private set; }
        public DateTime End { get; private set; }

        public BookingStatus Status { get; private set; }

        public Money TotalPrice { get; private set; }

        private Booking() { }

        private Booking(Guid venueId, Guid customerId, DateTime start, DateTime end, decimal pricePerHour, Currency currency)
        {
            Validate(start, end);

            Id = Guid.NewGuid();
            VenueId = venueId;
            CustomerId = customerId;
            Start = start;
            End = end;
            Status = BookingStatus.Pending;
            CreatedAt = DateTime.UtcNow;

            CalculatePrice(pricePerHour, currency);
        }
        public static Booking Create(
           Guid venueId, Guid customerId, DateTime start, DateTime end, decimal pricePerHour, Currency currency)
        {
            return new Booking(venueId, customerId, start, end, pricePerHour, currency);
        }

        private void Validate(DateTime start, DateTime end)
        {
            if (end <= start)
                throw new ArgumentException("End time must be after start time");

            if (start < DateTime.UtcNow)
                throw new ArgumentException("Cannot book in the past");
        }

        private void CalculatePrice(decimal pricePerHour, Currency currency)
        {
            var hours = (decimal)(End - Start).TotalHours;

            if (hours <= 0)
                throw new ArgumentException("Invalid booking duration");

            TotalPrice = new Money(hours * pricePerHour, currency);
        }

        public void Confirm()
        {
            if (Status != BookingStatus.Pending)
                throw new InvalidOperationException("Only pending bookings can be confirmed");

            Status = BookingStatus.Confirmed;
        }


        public BookingCancellation Cancel(CancellationPolicy policy, DateTime now)
        {
            if (Status == BookingStatus.Cancelled)
                throw new InvalidOperationException("Booking already cancelled");
            if (now >= Start)
                throw new InvalidOperationException("Cannot cancel after event start");
            decimal refundAmount = 0;
            if (policy != null)
            {
                var hoursBefore = (Start - now).TotalHours;

                if (hoursBefore >= policy.AllowedHoursBeforeEvent)
                {
                    refundAmount = TotalPrice.Amount * policy.RefundPercentage / 100;
                }
            }
            Status = BookingStatus.Cancelled;

            return new BookingCancellation
            {
                Id = Guid.NewGuid(),
                BookingId = Id,
                CancelledAt = now,
                RefundedAmount = new Money(refundAmount, TotalPrice.Currency)
            };
        }
    }
}
