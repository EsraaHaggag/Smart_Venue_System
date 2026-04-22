namespace Venue_System.Domain.ValueObjects
{
    public class WorkingHours
    {
        //public Guid Id { get; private set; }
        public DayOfWeek Day { get; private set; }
        public TimeSpan OpenFrom { get; private set; }
        public TimeSpan OpenTo { get; private set; }
        public bool IsClosed { get; private set; }

        private WorkingHours() { }

        public WorkingHours(DayOfWeek day, TimeSpan openFrom, TimeSpan openTo, bool isClosed)
        {
            if (!isClosed && openTo <= openFrom)
                throw new ArgumentException("Invalid working hours");

            Day = day;
            OpenFrom = openFrom;
            OpenTo = openTo;
            IsClosed = isClosed;
        }
    }
}
