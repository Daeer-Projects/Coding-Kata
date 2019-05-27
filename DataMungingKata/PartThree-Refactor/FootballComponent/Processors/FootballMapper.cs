using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using DataMungingCoreV2.Extensions;
using DataMungingCoreV2.Interfaces;
using DataMungingCoreV2.Types;
using FootballComponentV2.Constants;
using FootballComponentV2.Extensions;
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
            if (!fileData.IsValid<string[], StringArrayValidator>().IsValid) throw new InvalidDataException("Invalid Data File.");

            var results = await Task.Factory.StartNew(() =>
            {
                IList<IDataType> taskResults = new List<IDataType>();

                foreach (var item in fileData)
                {
                    // Need to use the config to extract out the items...
                    if (!item.Equals(FootballConstants.FootballHeader) && !item.Equals(FootballConstants.FootballDivider))
                    {
                        // So, not the header and not the divider.
                        var footballData = item.ToFootball();
                        if (footballData.IsValid)
                        {
                            _logger.Debug($"{GetType().Name} (MapAsync): Item valid: {item}.");
                            taskResults.Add(new ContainingDataType { Data = footballData.Football });
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
