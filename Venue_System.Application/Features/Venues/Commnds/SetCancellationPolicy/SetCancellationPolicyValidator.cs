using FluentValidation;

namespace Venue_System.Application.Features.Venues.Commnds.SetCancellationPolicy
{
    public class SetCancellationPolicyValidator : AbstractValidator<SetCancellationPolicyCommand>
    {
        public SetCancellationPolicyValidator()
        {
            RuleFor(x => x.AllowedHoursBeforeEvent)
                .GreaterThan(0);

            RuleFor(x => x.RefundPercentage)
                .InclusiveBetween(0, 100);
        }
    }
}
