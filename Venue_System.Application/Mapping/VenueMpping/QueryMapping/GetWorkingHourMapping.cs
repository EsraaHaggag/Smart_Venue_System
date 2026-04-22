using Venue_System.Application.Comman.DTO;
using Venue_System.Application.Features.DTO;
using Venue_System.Domain.ValueObjects;

namespace Venue_System.Application.Mapping.VenueMpping
{
    public partial class VenueProfile
    {
        public void GetWorkingHourMapping()
        {
            CreateMap<WorkingHours, WorkingHourDTo>();
        }
    }

}

