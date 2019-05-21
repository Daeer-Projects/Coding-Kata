using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using DataMungingCore.Interfaces;
using DataMungingCore.Types;
using Serilog;
using WeatherComponent.Extensions;
using WeatherComponent.Types;

namespace WeatherComponent.Processors
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

            var result = await Task.Factory.StartNew(() =>
            {
                var dayOfLeastChange = 0;
                var minimumTemperatureChange = float.MaxValue;
                foreach (var type in data)
                {
                    if (type.Data is Weather weather)
                    {
                        // Contract requirements. Duplicating the validation here. Should we?
                        var weatherValidationResult = weather.IsValid();
                        if (!weatherValidationResult.IsValid)
                        {
                            throw new ArgumentException(weatherValidationResult.Errors.Select(m => m.ErrorMessage).ToString());
                        }

                        var temperatureChange = weather.CalculateWeatherChange();
                        _logger.Debug($"{GetType().Name} (NotifyAsync): Temperature change calculated: {temperatureChange}.");

                        if (temperatureChange < minimumTemperatureChange)
                        {
                            minimumTemperatureChange = temperatureChange;
                            dayOfLeastChange = weather.Day;
                        }
                    }
                }

                IReturnType day = new ContainingResultType { ProcessResult = dayOfLeastChange };

                return day;
            }).ConfigureAwait(false);
            
            _logger.Information($"{GetType().Name} (NotifyAsync): Notification complete.");
            return result;
        }
    }
}
