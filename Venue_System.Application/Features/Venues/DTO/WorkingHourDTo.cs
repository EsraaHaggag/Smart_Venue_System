namespace Venue_System.Application.Features.DTO
{
    public class WorkingHourDTo
    {
        public DayOfWeek Day { get; set; }
        public TimeSpan OpenFrom { get; set; }
        public TimeSpan OpenTo { get; set; }
        public bool IsClosed { get; set; }
    }
}
