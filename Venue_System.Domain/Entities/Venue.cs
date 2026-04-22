using Venue_System.Domain.Domain.Entities;
using Venue_System.Domain.Entities;
using Venue_System.Domain.Enums;
using Venue_System.Domain.ValueObjects;

public class Venue : CommonData
{
    public Guid Id { get; private set; }
    public Guid OwnerId { get; private set; }
    public VenueOwner Owner { get; set; }
    public string Name { get; private set; }
    public string Description { get; private set; }
    public VenueLocation Location { get; private set; }
    public Money BaseHourlyPrice { get; private set; }
    public int Capacity { get; private set; }
    //public WorkingHours AvailableTime { get; private set; }

    public bool IsActive { get; private set; } = true;

    private readonly List<WorkingHours> _workingHours = new();
    public IReadOnlyCollection<WorkingHours> WorkingHours => _workingHours;

    private readonly List<VenueRule> _rules = new List<VenueRule>();
    public virtual IReadOnlyCollection<VenueRule> Rules => _rules.AsReadOnly();

    public Guid? CancellationPolicyId { get; set; }

    public CancellationPolicy? CancellationPolicy { get; set; }

    public Guid AddRule(string text, bool isMandatory)
    {
        if (_rules.Any(r => r.RuleText == text))
            throw new InvalidOperationException("Rule already exists");
        var rule = new VenueRule(text, isMandatory);
        _rules.Add(rule);
        return rule.Id;
    }

    public void RemoveRule(Guid ruleId)
    {
        var rule = _rules.FirstOrDefault(r => r.Id == ruleId);

        if (rule == null)
            throw new InvalidOperationException("Rule not found");

        rule.SoftDelete();
    }

    public void UpdateRule(Guid ruleId, string text, bool isMandatory)
    {
        var rule = _rules.FirstOrDefault(r => r.Id == ruleId);

        if (rule == null)
            throw new InvalidOperationException("Rule not found");

        rule.Update(text, isMandatory);
    }

    private Venue() { }

    private Venue(
    string name,
    string description,
    VenueLocation location,
    int capacity,
    Money money,
    Guid ownerId)
    {
        Validate(name, description, location, capacity, money);


        Id = Guid.NewGuid();
        Name = name;
        Description = description;
        Location = location;
        Capacity = capacity;
        BaseHourlyPrice = money;
        OwnerId = ownerId;
    }

    public static Venue Register(string name, string description, VenueLocation location,
        int capacity, Money money, Guid ownerId)
    {
        return new Venue(name, description, location,
        capacity, money, ownerId);
    }

    public void Update(string name, string description, VenueLocation location,
        int capacity, Money money)
    {
        Validate(name, description, location, capacity, money);
        Name = name;
        Description = description;
        Location = location;
        Capacity = capacity;
        BaseHourlyPrice = money;
        UpdatedAt = DateTime.UtcNow;
    }

    private void Validate(
    string name,
    string description,
    VenueLocation location,
    int capacity,
    Money money)
    {

        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Name is required");

        if (name.Length > 200)
            throw new ArgumentException("Name is too long");


        if (!string.IsNullOrWhiteSpace(description) && description.Length > 1000)
            throw new ArgumentException("Description is too long");

        if (location is null)
            throw new ArgumentNullException(nameof(location));

        if (capacity <= 0)
            throw new ArgumentException("Capacity must be greater than 0");

        if (money is null)
            throw new ArgumentNullException(nameof(money));
    }

    public decimal CalculatePrice(int hours)
    {
        if (hours <= 0)
            throw new ArgumentException("Invalid hours");

        return BaseHourlyPrice.Amount * hours;
    }
    public void ChangePrice(Money newPrice)
    {
        if (newPrice == null)
            throw new ArgumentNullException(nameof(newPrice));

        BaseHourlyPrice = newPrice;
    }
    public void Deactivate()
    {
        IsActive = false;
    }

    public void Activate()
    {
        IsActive = true;
    }

    public void AddWorkingHours(DayOfWeek day, TimeSpan from, TimeSpan to, bool isClosed)
    {
        if (from >= to)
            throw new ArgumentException("Invalid time range");
        if (_workingHours.Any(w => w.Day == day))
            throw new Exception("Working hours for this day already exist");

        _workingHours.Add(new WorkingHours(day, from, to, isClosed));
    }

    public void UpdateWorkingHours(DayOfWeek day, TimeSpan from, TimeSpan to, bool isClosed)
    {
        var existing = _workingHours.FirstOrDefault(w => w.Day == day);

        if (existing == null)
            throw new Exception("Working hours not found");

        _workingHours.Remove(existing);
        _workingHours.Add(new WorkingHours(day, from, to, isClosed));
    }

    public bool IsAvailable(DateTime start, DateTime end, List<Booking> existingBookings)
    {
        if (end <= start)
            throw new ArgumentException("Invalid time range");

        if (start.Date != end.Date)
            throw new ArgumentException("Booking must be within one day");

        var day = start.DayOfWeek;

        var working = _workingHours.FirstOrDefault(w => w.Day == day);

        if (working == null || working.IsClosed)
            return false;

        if (start.TimeOfDay < working.OpenFrom || end.TimeOfDay > working.OpenTo)
            return false;

        foreach (var booking in existingBookings)
        {
            if (booking.Status == BookingStatus.Cancelled)
                continue;

            bool isOverlapping =
                start < booking.End &&
                end > booking.Start;

            if (isOverlapping)
                return false;
        }

        return true;
    }

    public CancellationPolicy SetCancellationPolicy(int hours, decimal percentage)
    {
        return CancellationPolicy = new CancellationPolicy(hours, percentage);
    }
}
