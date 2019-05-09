using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

using DataMungingCore.Interfaces;
using DataMungingCore.Types;
using WeatherComponent.Constants;
using WeatherComponent.Extensions;

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

                // ToDo: Convert to Linq when we have the logging sorted.
                foreach (var item in fileData)
                {
                    // Need to use the config to extract out the items...
                    if (!item.Equals(WeatherConstants.WeatherHeader) && !string.IsNullOrWhiteSpace(item) && !item.Contains("mo"))
                    {
                        // So, not the header and not the empty line.
                        var weatherData = item.ToWeather();
                        if (weatherData.IsValid)
                        {
                            taskResults.Add(new ContainingDataType
                                {Data = weatherData.Weather});
                        }
                        else
                        {
                            // Do some logging here when we sort that out.
                        }

                    }
                }

                return taskResults;
            });

            return results;
        }
    }
}
