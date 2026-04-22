using Venue_System.Application.Features.Venues.Commnds.CreateVenue;
namespace Venue_System.Application.Mapping.VenueMpping
{
    public partial class VenueProfile
    {
        public void CreateVenueMapping()
        {
            CreateMap<CreateVenueCommand, Venue>();
        }
    }

}
