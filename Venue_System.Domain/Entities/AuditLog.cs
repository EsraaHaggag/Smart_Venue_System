namespace Venue_System.Domain.Entities
{
    public class AuditLog
    {
        public Guid Id { get; set; }
        public string Action { get; set; }
        public Guid PerformedBy { get; set; }
        public DateTime PerformedAt { get; set; }
    }

}
