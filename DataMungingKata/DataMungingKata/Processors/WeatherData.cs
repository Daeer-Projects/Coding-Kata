using System;
using System.Collections.Generic;

using DataMungingKata.Types;

namespace DataMungingKata.Processors
{
    public class WeatherData
    {
        public int GetDayOfLeastTemperatureChange(IList<Weather> weatherData)
        {
            // Contract requirements.
            if (weatherData is null) throw new ArgumentNullException(nameof(weatherData), "The weather data can not be null.");
            if (weatherData.Count < 1) throw new ArgumentException(nameof(weatherData), "The weather data must contain data.");

            var dayOfLeastChange = 0;
            var minimumTemperatureChange = float.MaxValue;

            foreach (var weather in weatherData)
            {
                var temperatureChange = weather.MaximumTemperature - weather.MinimumTemperature;

                if (temperatureChange < minimumTemperatureChange)
                {
                    minimumTemperatureChange = temperatureChange;
                    dayOfLeastChange = weather.Day;
                }
            }

            return dayOfLeastChange;
        }
    }
}
