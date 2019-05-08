using WeatherComponent.Types;
using WeatherComponent.Validators;

namespace WeatherComponent.Extensions
{
    public static class WeatherExtensions
    {
        public static bool IsValid(this Weather weather)
        {
            var validator = new WeatherValidator();

            var result = validator.Validate(weather);

            return result.IsValid;
        }
    }
}
