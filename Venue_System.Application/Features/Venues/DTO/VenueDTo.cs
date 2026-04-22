using Venue_System.Application.Comman.DTO;
using Venue_System.Application.Features.DTO;

namespace Venue_System.Application.Features.Venues.DTO
{
    public class VenueDTo
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public VenueLocationDTo Location { get; set; }
        public MoneyDTo BaseHourlyPrice { get; set; }
        public int Capacity { get; set; }
        public TimeSlotDTo AvailableTime { get; set; }
        public bool IsActive { get; set; }
        public List<VenueRuleDTo> Rules { get; set; }
        public List<WorkingHourDTo> WorkingHours { get; set; }
    }
}
