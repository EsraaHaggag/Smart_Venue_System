using Microsoft.AspNetCore.SignalR;
namespace Venue_System.API.Web.Hubs
{

    public class VenueHub : Hub
    {
        public async Task JoinVenueGroup(string venueId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, $"venue-{venueId}");
        }

        public async Task LeaveVenueGroup(string venueId)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, $"venue-{venueId}");
        }
    }
}
