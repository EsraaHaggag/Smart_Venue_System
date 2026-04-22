namespace Venue_System.Domain.Entities
{
    public class VenueOwner
    {
        public Guid Id { get; set; }
        public ICollection<Venue> Venues { get; set; } = new List<Venue>();

        public VenueOwner(Guid id)
        {
            Id = id;
        }
    }
}
