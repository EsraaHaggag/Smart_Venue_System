namespace Venue_System.Domain.Entities
{
    public class Customer
    {
        public Guid Id { get; set; }

        public Customer(Guid id)
        {
            Id = id;
        }
    }
}
