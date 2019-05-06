using WeatherComponent.Validators;

namespace WeatherComponent.Extensions
{
    public static class StringArrayExtensions
    {
        public static bool IsValid(this string[] data)
        {
            var validator = new StringArrayValidator();

            var result = validator.Validate(data);

            return result.IsValid;
        }
    }
}
