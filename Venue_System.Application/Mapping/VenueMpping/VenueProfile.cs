using AutoMapper;

namespace Venue_System.Application.Mapping.VenueMpping
{
    public partial class VenueProfile : Profile
    {
        public VenueProfile()
        {
            CreateVenueMapping();
            GetVenueMapping();
        }

    }
}
