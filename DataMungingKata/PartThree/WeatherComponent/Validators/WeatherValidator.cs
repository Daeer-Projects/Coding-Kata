using FluentValidation;
using WeatherComponent.Types;

namespace WeatherComponent.Validators
{
    public class WeatherValidator : AbstractValidator<Weather>
    {
        public WeatherValidator()
        {
            RuleFor(weather => weather).NotNull();
            RuleFor(weather => weather.Day).InclusiveBetween(1, 31).WithMessage("Days must be within 1 and 31.");
            RuleFor(weather => weather).Must(MinimumTempMustBeLessThanMaximumTemp)
                .WithMessage("The Minimum Temperature cannot be greater than the Maximum Temperature.");
        }

        private bool MinimumTempMustBeLessThanMaximumTemp(Weather weather)
        {
            return weather.MinimumTemperature <= weather.MaximumTemperature;
        }
    }
}
