﻿using FluentValidation.Results;
using WeatherComponent.Types;
using WeatherComponent.Validators;

namespace WeatherComponent.Extensions
{
    public static class WeatherExtensions
    {
        public static ValidationResult IsValid(this Weather weather)
        {
            var validator = new WeatherValidator();

            var result = validator.Validate(weather);

            return result;
        }

        /// <summary>
        /// Extracted out this simple calculation so that if it changes
        /// in the future, we only change it in one place.
        /// </summary>
        /// <param name="weather"> The item that contains the min and max temperatures. </param>
        /// <returns>
        /// The calculated temperature change.
        /// </returns>
        public static float CalculateWeatherChange(this Weather weather)
        {
            return weather.MaximumTemperature - weather.MinimumTemperature;
        }
    }
}
