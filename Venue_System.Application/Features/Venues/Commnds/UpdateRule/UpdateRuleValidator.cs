using FluentValidation;
using Venue_System.Application.Features.Venues.Commnds.RemoveRule;

namespace Venue_System.Application.Features.Venues.Commnds.UpdateRule
{
    public class UpdateRuleValidator : AbstractValidator<RemoveRuleCommand>
    {
        public UpdateRuleValidator()
        {
            RuleFor(x => x.VenueId)
                    .NotEmpty()
                    .WithMessage("Venue Id is Required");
            RuleFor(x => x.RuleId)
                    .NotEmpty()
                    .WithMessage("Rule Id is Required");
        }

    }
}
