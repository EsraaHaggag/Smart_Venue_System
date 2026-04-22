using FluentValidation;
namespace Venue_System.Application.Features.Venues.Commnds.RemoveRule
{
    public class RemoveRuleValidator : AbstractValidator<RemoveRuleCommand>
    {
        public RemoveRuleValidator()
        {
            RuleFor(x => x.VenueId)
                    .NotEmpty();
            RuleFor(x => x.RuleId)
                    .NotEmpty();
        }
    }
}
