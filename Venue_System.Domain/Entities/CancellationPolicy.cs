namespace Venue_System.Domain.Entities
{
    public class CancellationPolicy
    {
        public CancellationPolicy(int allowedHoursBeforeEvent,
          decimal refundPercentage)
        {
            if (allowedHoursBeforeEvent <= 0)
                throw new Exception("AllowedHoursBeforeEvent Must be greater than 0");
            if (refundPercentage < 0 || refundPercentage > 100)
                throw new ArgumentException("Invalid percentage");

            Id = Guid.NewGuid();
            AllowedHoursBeforeEvent = allowedHoursBeforeEvent;
            RefundPercentage = refundPercentage;
        }

        public Guid Id { get; set; }
        public int AllowedHoursBeforeEvent { get; set; }
        public decimal RefundPercentage { get; set; }
    }

}
