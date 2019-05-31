using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using DataMungingCoreV2.Extensions;
using DataMungingCoreV2.Interfaces;
using DataMungingCoreV2.Processors;
using Serilog;
using WeatherComponentV2.Extensions;
using WeatherComponentV2.Types;
using WeatherComponentV2.Validators;

namespace WeatherComponentV2.Processors
{
    public class WeatherWriter : IWriter
    {
        private readonly ILogger _logger;

        public WeatherWriter(ILogger logger)
        {
            _logger = logger;
        }

        public async Task<IReturnType> WriteAsync(IList<IDataType> data)
        {
            _logger.Information($"{GetType().Name} (WriteAsync): Starting to calculate the result.");

            // Contract requirements.
            if (data is null) throw new ArgumentNullException(nameof(data), "The weather data can not be null.");
            if (data.Count < 1) throw new ArgumentException("The weather data must contain data.");

            var result = await Writer.WriteWork<Weather, float, int>(data, (float.MaxValue, 0), CurrentRange)
                .ConfigureAwait(false);


            _logger.Information($"{GetType().Name} (WriteAsync): Notification complete.");
            return result;
        }
        
        private (float, int) CurrentRange<T>((float, int) currentRange, T componentType) where T : class
        {
            // Casting to expected type.
            var specificType = componentType as Weather;

            // Contract requirements. Duplicating the validation here. Should we?
            ValidationConfirmation(specificType);

            var temperatureChange = specificType.CalculateWeatherChange();
            _logger.Debug($"{GetType().Name} (WriteAsync): Temperature change calculated: {temperatureChange}.");

            currentRange = EvaluateData(currentRange, temperatureChange, specificType);

            return currentRange;
        }

        private static (float, int) EvaluateData((float, int) currentRange, float temperatureChange, Weather specificType)
        {
            if (temperatureChange < currentRange.Item1)
            {
                currentRange.Item1 = temperatureChange;
                currentRange.Item2 = specificType.Day;
            }

            return currentRange;
        }

        private static void ValidationConfirmation(Weather weather)
        {
            var weatherValidationResult = weather.IsValid(new WeatherValidator());
            if (!weatherValidationResult.IsValid)
            {
                throw new ArgumentException(weatherValidationResult.Errors.Select(m => m.ErrorMessage).ToString());
            }
        }
    }
}
