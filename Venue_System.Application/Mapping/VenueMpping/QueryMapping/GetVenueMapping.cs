using Venue_System.Application.Comman.DTO;
using Venue_System.Application.Features.DTO;
using Venue_System.Application.Features.Venues.DTO;
using Venue_System.Domain.Entities;
using Venue_System.Domain.ValueObjects;

namespace Venue_System.Application.Mapping.VenueMpping
{
    public partial class VenueProfile
    {
        public void GetVenueMapping()
        {
            CreateMap<Venue, VenueDTo>();

            CreateMap<VenueRule, VenueRuleDTo>();

            CreateMap<VenueLocation, VenueLocationDTo>();

            CreateMap<Money, MoneyDTo>();
            CreateMap<WorkingHours, WorkingHourDTo>();
            CreateMap<TimeSlot, TimeSlotDTo>();
        }
    }

}
