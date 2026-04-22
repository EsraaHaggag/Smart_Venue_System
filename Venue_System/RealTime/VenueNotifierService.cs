using Microsoft.AspNetCore.SignalR;
using Venue_System.API.Web.Hubs;
using Venue_System.Application.Interfaces.Services;

namespace Venue_System.API.RealTime
{
    public class VenueNotifier : IVenueNotifierService
    {
        private readonly IHubContext<VenueHub> _hub;

        public VenueNotifier(IHubContext<VenueHub> hub)
        {
            _hub = hub;
        }

        public async Task NotifyBooked(Guid venueId, DateTime start, DateTime end)
        {
            await _hub.Clients.Group($"venue-{venueId}")
                .SendAsync("VenueBooked", new
                {
                    venueId = venueId,
                    start = start,
                    end = end
                });
        }

        public async Task NotifyBookingCancelled(Guid venueId, DateTime start, DateTime end)
        {
            await _hub.Clients.Group($"venue-{venueId}")
                .SendAsync("BookingCancelled", new
                {
                    venueId = venueId,
                    start = start,
                    end = end
                });
        }
    }
}

