using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using DataMungingCoreV2.Extensions;
using DataMungingCoreV2.Interfaces;
using DataMungingCoreV2.Types;
using Serilog;
using WeatherComponentV2.Constants;
using WeatherComponentV2.Extensions;
using WeatherComponentV2.Validators;

namespace WeatherComponentV2.Processors
{
    public class WeatherMapper : IMapper
    {
        private readonly ILogger _logger;

        public WeatherMapper(ILogger logger)
        {
            _logger = logger;
        }

        public async Task<IList<IDataType>> MapAsync(string[] fileData)
        {
            _logger.Information($"{GetType().Name} (MapAsync): Starting to map the data.");

            // Convert this to a type with specific validation.
            // We want to check the file has a header, an empty row, a footer and at least one row with data in it.
            if (!fileData.IsValid<string[], StringArrayValidator>().IsValid) throw new InvalidDataException("Invalid Data File.");

            var results = await Task.Factory.StartNew(() =>
            {
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
                            _logger.Debug($"{GetType().Name} (MapAsync): Item valid: {item}.");
                            taskResults.Add(new ContainingDataType
                            { Data = weatherData.Weather });
                        }
                        else
                        {
                            // Do some logging here when we sort that out.
                            _logger.Warning($"{GetType().Name} (MapAsync): Item not valid: {item}.");
                        }
                    }
                }

                return taskResults;
            }).ConfigureAwait(false);
            
            _logger.Information($"{GetType().Name} (MapAsync): Mapping complete.");
            return results;
        }
    }
}
