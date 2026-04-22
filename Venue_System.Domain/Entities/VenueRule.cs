using Venue_System.Domain.Domain.Entities;

namespace Venue_System.Domain.Entities
{
    public class VenueRule : CommonData
    {
        public Guid Id { get; private set; }
        public string RuleText { get; set; }
        public bool IsMandatory { get; set; }
        public Guid VenueId { get; private set; }
        public Venue Venue { get; private set; }

        public VenueRule() { }
        public VenueRule(string text, bool isMandatory)
        {
            if (string.IsNullOrWhiteSpace(text))
                throw new ArgumentException("Rule text is required");
            Id = Guid.NewGuid();
            RuleText = text;
            IsMandatory = isMandatory;
            CreatedAt = DateTime.UtcNow;
        }

        public void Update(string text, bool isMandatory)
        {
            if (string.IsNullOrWhiteSpace(text))
                throw new ArgumentException("Text is required");

            RuleText = text;
            IsMandatory = isMandatory;
            UpdatedAt = DateTime.UtcNow;
        }
    }

}
