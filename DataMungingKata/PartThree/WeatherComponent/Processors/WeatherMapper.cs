using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

using DataMungingCore.Interfaces;
using DataMungingCore.Types;
using WeatherComponent.Configuration;
using WeatherComponent.Constants;
using WeatherComponent.Extensions;
using WeatherComponent.Types;

namespace WeatherComponent.Processors
{
    public class WeatherMapper : IMapper
    {
        public async Task<IList<IDataType>> MapAsync(string[] fileData)
        {
            // Convert this to a type with specific validation.
            // We want to check the file has a header, an empty row, a footer and at least one row with data in it.
            if (!fileData.IsValid()) throw new InvalidDataException("Invalid Data File.");
            
            var results = await Task.Factory.StartNew(() => {
                IList<IDataType> taskResults = new List<IDataType>();
                foreach (var item in fileData)
                {
                    // Need to use the config to extract out the items...
                    if (!item.Equals(WeatherConstants.WeatherHeader) && !string.IsNullOrWhiteSpace(item) && !item.Contains("mo"))
                    {
                        // So, not the header and not the empty line.
                        var day = item.Substring(WeatherConfig.DayColumnStart, WeatherConfig.DayColumnLength);
                        var maxTemp = item.Substring(WeatherConfig.MaxTempColumnStart, WeatherConfig.MaxTempColumnLength);
                        var minTemp = item.Substring(WeatherConfig.MinTempColumnStart, WeatherConfig.MinTempColumnLength);

                        if (int.TryParse(day, out var dayAsInt) &&
                            float.TryParse(maxTemp, out var maxTempAsFloat) &&
                            float.TryParse(minTemp, out var minTempAsFloat))
                        {
                            // So all of them parsed correctly.
                            var data = new ContainingDataType
                            {
                                Data = new Weather
                                {
                                    Day = dayAsInt,
                                    MaximumTemperature = maxTempAsFloat,
                                    MinimumTemperature = minTempAsFloat
                                }
                            };

                            taskResults.Add(data);
                        }
                    }
                }

                return taskResults;
            });

            return results;
        }
    }
}
