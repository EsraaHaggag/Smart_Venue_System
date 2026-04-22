namespace Venue_System.Application.Interfaces.Services
{
    public interface IVenueNotifierService
    {
        Task NotifyBooked(Guid venueId, DateTime start, DateTime end);

        Task NotifyBookingCancelled(Guid venueId, DateTime start, DateTime end);
    }
}
