namespace Venue_System.Domain.ValueObjects
{
    public class TimeSlot
    {
        public TimeSpan Start { get; }
        public TimeSpan End { get; }
        private TimeSlot() { }
        public TimeSlot(TimeSpan start, TimeSpan end)
        {
            if (start < TimeSpan.Zero || start >= TimeSpan.FromDays(1))
                throw new ArgumentException("Start must be between 00:00 and 23:59");

            if (end < TimeSpan.Zero || end >= TimeSpan.FromDays(1))
                throw new ArgumentException("End must be between 00:00 and 23:59");

            if (end < start)
                throw new ArgumentException("Invalid time range (overnight not supported)");


            Start = start;
            End = end;
        }
    }
}

