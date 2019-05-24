using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using DataMungingCore.Interfaces;
using DataMungingCore.Types;
using FootballComponent.Extensions;
using FootballComponent.Types;
using Serilog;

namespace FootballComponent.Processors
{
    /// <summary>
    /// The notifier returns the results of answering the question about the data.
    /// </summary>
    public class FootballNotifier : INotify
    {
        private readonly ILogger _logger;

        public FootballNotifier(ILogger logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Identifies the team that has the least point difference.
        /// </summary>
        /// <param name="data"> The football data we are asking the question against. </param>
        /// <returns> The result of asking the question. </returns>
        public async Task<IReturnType> NotifyAsync(IList<IDataType> data)
        {
            _logger.Information($"{GetType().Name} (NotifyAsync): Starting to calculate the result.");

            // Contract requirements.
            if (data is null) throw new ArgumentNullException(nameof(data), "The football data can not be null.");
            if (data.Count < 1) throw new ArgumentException("The football data must contain data.");

            var result = await Task.Factory.StartNew(() =>
            {
                var teamWithSmallestPointRange = string.Empty;
                var smallestRange = int.MaxValue;

                foreach (var type in data)
                {
                    if (type.Data is Football football)
                    {
                        // Contract requirements. Duplicating the validation here. Should we?
                        var footballValidationResult = football.IsValid();
                        if (!footballValidationResult.IsValid)
                        {
                            throw new ArgumentException(footballValidationResult.Errors.Select(m => m.ErrorMessage).ToString());
                        }

                        var range = football.CalculatePointDifference();

                        if (range < smallestRange)
                        {
                            smallestRange = range;
                            teamWithSmallestPointRange = football.TeamName;
                        }
                    }
                }

                IReturnType team = new ContainingResultType { ProcessResult = teamWithSmallestPointRange };

                return team;
            }).ConfigureAwait(false);

            _logger.Information($"{GetType().Name} (NotifyAsync): Notification complete.");
            return result;
        }
    }
}
