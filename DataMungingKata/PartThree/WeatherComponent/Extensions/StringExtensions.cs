using WeatherComponent.Configuration;
using WeatherComponent.Types;

namespace WeatherComponent.Extensions
{
    public static class StringExtensions
    {
        public static Weather ToWeather(this string item)
        {
            Weather result = null;

            var day = item.Substring(WeatherConfig.DayColumnStart, WeatherConfig.DayColumnLength);
            var maxTemp = item.Substring(WeatherConfig.MaxTempColumnStart, WeatherConfig.MaxTempColumnLength);
            var minTemp = item.Substring(WeatherConfig.MinTempColumnStart, WeatherConfig.MinTempColumnLength);

            if (int.TryParse(day, out var dayAsInt) &&
                float.TryParse(maxTemp, out var maxTempAsFloat) &&
                float.TryParse(minTemp, out var minTempAsFloat))
            {
                result = new Weather
                {
                    Day = dayAsInt,
                    MaximumTemperature = maxTempAsFloat,
                    MinimumTemperature = minTempAsFloat
                };
            }

            return result;
        }
    }
}
