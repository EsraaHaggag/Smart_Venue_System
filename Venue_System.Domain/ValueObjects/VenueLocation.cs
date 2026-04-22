namespace Venue_System.Domain.ValueObjects
{
    public class VenueLocation
    {
        public string Country { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public string GoogleMapUrl { get; set; }
        public VenueLocation() { }
        public VenueLocation(string country, string city, string address, string googleMapUrl)
        {
            if (string.IsNullOrWhiteSpace(country))
                throw new ArgumentException("Country is required");

            if (string.IsNullOrWhiteSpace(city))
                throw new ArgumentException("City is required");

            if (string.IsNullOrWhiteSpace(address))
                throw new ArgumentException("Address is required");

            if (country.Length > 100)
                throw new ArgumentException("Country is too long");

            if (city.Length > 100)
                throw new ArgumentException("City is too long");

            if (address.Length > 400)
                throw new ArgumentException("Address is too long");

            //if (!string.IsNullOrWhiteSpace(googleMapUrl))
            //{
            //    if (!Uri.IsWellFormedUriString(googleMapUrl, UriKind.Absolute))
            //        throw new ArgumentException("Invalid Google Map URL");
            //}

            Country = country;
            City = city;
            Address = address;
            GoogleMapUrl = googleMapUrl;
        }
    }

}
