using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using DataMungingCore.Interfaces;
using DataMungingCore.Types;
using WeatherComponent.Types;

namespace WeatherComponent.Processors
{
    public class WeatherNotifier : INotify
    {
        public async Task<IReturnType> NotifyAsync(IList<IDataType> data)
        {
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
                        // Contract requirements. ToDo: Extract out to another method.
                        if (weather.MinimumTemperature > weather.MaximumTemperature) throw new ArgumentException("The minimum temperature can not be greater than the maximum temperature.");

                        var temperatureChange = weather.MaximumTemperature - weather.MinimumTemperature;

                        if (temperatureChange < minimumTemperatureChange)
                        {
                            minimumTemperatureChange = temperatureChange;
                            dayOfLeastChange = weather.Day;
                        }
                    }
                }

                IReturnType day = new ContainingResultType { Result = dayOfLeastChange };

                return day;
            });

            return result;
        }
    }
}
