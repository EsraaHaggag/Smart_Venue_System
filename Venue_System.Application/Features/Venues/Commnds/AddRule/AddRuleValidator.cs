using FluentValidation;

namespace Venue_System.Application.Features.Venues.Commnds.AddRule
{
    public class AddRuleValidator
        : AbstractValidator<AddRuleCommand>
    {
        public AddRuleValidator()
        {
            RuleFor(x => x.RuleText)
                .NotEmpty();

            RuleFor(x => x.IsMandatory)
                    .NotEmpty();

        }
    }
}
