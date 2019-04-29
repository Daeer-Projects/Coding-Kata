using System;
using System.Collections.Generic;

using DataMungingKata.Interfaces;
using DataMungingKata.Types;

namespace DataMungingKata.Processors
{
    public class WeatherData : INotify
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
                // Contract requirements. ToDo: Extract out to another method.
                if (weather.MinimumTemperature > weather.MaximumTemperature) throw new ArgumentException(nameof(weather), "The minimum temperature can not be greater than the maximum temperature.");

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
