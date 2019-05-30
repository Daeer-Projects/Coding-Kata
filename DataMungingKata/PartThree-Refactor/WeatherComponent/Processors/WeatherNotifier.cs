using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using DataMungingCoreV2.Extensions;
using DataMungingCoreV2.Interfaces;
using DataMungingCoreV2.Types;
using Serilog;
using WeatherComponentV2.Extensions;
using WeatherComponentV2.Types;
using WeatherComponentV2.Validators;

namespace WeatherComponentV2.Processors
{
    public class WeatherNotifier : INotify
    {
        private readonly ILogger _logger;

        public WeatherNotifier(ILogger logger)
        {
            _logger = logger;
        }

        public async Task<IReturnType> NotifyAsync(IList<IDataType> data)
        {
            _logger.Information($"{GetType().Name} (NotifyAsync): Starting to calculate the result.");

            // Contract requirements.
            if (data is null) throw new ArgumentNullException(nameof(data), "The weather data can not be null.");
            if (data.Count < 1) throw new ArgumentException("The weather data must contain data.");

            var result = await Task.Factory.StartNew(() => NotificationWork(data)).ConfigureAwait(false);
            
            _logger.Information($"{GetType().Name} (NotifyAsync): Notification complete.");
            return result;
        }

        private IReturnType NotificationWork(IEnumerable<IDataType> data)
        {
            var (_, dayOfLeastChange) =
                data.Aggregate((float.MaxValue, 0), (current, type) => SmallestRange<Weather>(type, current));

            IReturnType day = new ContainingResultType {ProcessResult = dayOfLeastChange};

            return day;
        }

        private (float, int) SmallestRange<T>(IDataType type, (float, int) currentRange) where T : class
        {
            if (type.Data is T weather)
            {
                currentRange = CurrentRange(currentRange, weather);
            }

            return currentRange;
        }

        private (float, int) CurrentRange<T>((float, int) currentRange, T componentType) where T : class
        {
            // Casting to expected type.
            var specificType = componentType as Weather;

            // Contract requirements. Duplicating the validation here. Should we?
            ValidationConfirmation(specificType);

            var temperatureChange = specificType.CalculateWeatherChange();
            _logger.Debug($"{GetType().Name} (NotifyAsync): Temperature change calculated: {temperatureChange}.");

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
