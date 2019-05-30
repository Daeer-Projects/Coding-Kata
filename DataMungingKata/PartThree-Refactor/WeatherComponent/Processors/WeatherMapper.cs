using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

using DataMungingCoreV2.Extensions;
using DataMungingCoreV2.Interfaces;
using DataMungingCoreV2.Processors;
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
            if (!fileData.IsValid(new StringArrayValidator()).IsValid) throw new InvalidDataException("Invalid Data File.");

            var results = await Mapper.MapWork(fileData, CheckItemRow, AddDataItem).ConfigureAwait(false);
            
            _logger.Information($"{GetType().Name} (MapAsync): Mapping complete.");
            return results;
        }

        private static bool CheckItemRow(string item)
        {
            return !item.Equals(WeatherConstants.WeatherHeader) && 
                   !string.IsNullOrWhiteSpace(item) &&
                   !item.Contains("mo");
        }

        private IList<IDataType> AddDataItem(string item, IList<IDataType> results)
        {
            var dataResults = results;
            var weatherData = item.ToWeather();
            if (weatherData.IsValid)
            {
                _logger.Debug($"{GetType().Name} (MapAsync): Item valid: {item}.");
                dataResults.Add(new ContainingDataType { Data = weatherData.Weather });
            }
            else
            {
                _logger.Warning($"{GetType().Name} (MapAsync): Item not valid: {item}.");
            }

            return dataResults;
        }
    }
}
