using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DataMungingCoreV2.Extensions;
using DataMungingCoreV2.Interfaces;
using DataMungingCoreV2.Processors;
using DataMungingCoreV2.Types;
using FootballComponentV2.Constants;
using FootballComponentV2.Extensions;
using FootballComponentV2.Types;
using FootballComponentV2.Validators;
using Serilog;

namespace FootballComponentV2.Processors
{
    /// <summary>
    /// The football mapper.
    /// </summary>
    public class FootballMapper : IMapper
    {
        private readonly ILogger _logger;

        public FootballMapper(ILogger logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Maps the data into football objects.
        /// </summary>
        /// <param name="fileData"> The data we are converting to football objects. </param>
        /// <returns> A collection of football objects. </returns>
        public async Task<IList<IDataType>> MapAsync(string[] fileData)
        {
            _logger.Information($"{GetType().Name} (MapAsync): Starting to map the data.");

            // Convert this to a type with specific validation.
            // We want to check the file has a header, an empty row, a footer and at least one row with data in it.
            if (!fileData.IsValid(new StringArrayValidator()).IsValid) throw new InvalidDataException("Invalid Data File.");

            var results = await Mapper.MapWork(() => MapDataToResults(fileData)).ConfigureAwait(false);
            //var results = await Mapper.ExperimentalMapWork(fileData, CheckItemRow, AddDataItem).ConfigureAwait(false);

            _logger.Information($"{GetType().Name} (MapAsync): Mapping complete.");
            return results;
        }

        private IList<IDataType> MapDataToResults(string[] fileData)
        {
            IList<IDataType> taskResults = new List<IDataType>();

            return fileData.Aggregate(taskResults, (current, item) => AddData(item, current));
        }

        private IList<IDataType> AddData(string item, IList<IDataType> taskResults)
        {
            var results = taskResults;
            if (CheckItemRow(item))
            {
                // So, not the header and not the divider.
                results = AddDataItem(item, results);
            }

            return results;
        }

        private IList<IDataType> AddDataItem(string item, IList<IDataType> results)
        {
            var dataResults = results;
            var footballData = item.ToFootball();
            if (footballData.IsValid)
            {
                _logger.Debug($"{GetType().Name} (MapAsync): Item valid: {item}.");
                dataResults.Add(new ContainingDataType {Data = footballData.Football});
            }
            else
            {
                _logger.Warning($"{GetType().Name} (MapAsync): Item not valid: {item}.");
            }

            return dataResults;
        }

        private static bool CheckItemRow(string item)
        {
            return !item.Equals(FootballConstants.FootballHeader) && !item.Equals(FootballConstants.FootballDivider);
        }
    }
}
