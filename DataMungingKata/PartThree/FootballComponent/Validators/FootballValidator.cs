using FluentValidation;
using FootballComponent.Types;

namespace FootballComponent.Validators
{
    public class FootballValidator : AbstractValidator<Football>
    {
        public FootballValidator()
        {
            RuleFor(football => football).NotNull();
            RuleFor(football => football.TeamName).Must(TeamNameMustNotBeNullOrWhiteSpace).WithMessage("Team must have a name.");
            RuleFor(football => football.ForPoints).GreaterThanOrEqualTo(0).WithMessage(PointsMustBePositive);
            RuleFor(football => football.AgainstPoints).GreaterThanOrEqualTo(0).WithMessage(PointsMustBePositive);
        }

        private const string PointsMustBePositive = "Points must be positive.  They can't be less than 0.";

        private bool TeamNameMustNotBeNullOrWhiteSpace(string teamName)
        {
            return !string.IsNullOrWhiteSpace(teamName);
        }
    }
}
