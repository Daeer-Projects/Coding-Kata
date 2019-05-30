using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using DataMungingCoreV2.Extensions;
using DataMungingCoreV2.Interfaces;
using DataMungingCoreV2.Processors;
using DataMungingCoreV2.Types;
using FootballComponentV2.Extensions;
using FootballComponentV2.Types;
using FootballComponentV2.Validators;
using Serilog;

namespace FootballComponentV2.Processors
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
            
            var result = await Notify.NotificationWork<Football, int, string>(data, (int.MaxValue, string.Empty), CurrentRange)
                .ConfigureAwait(false);

            _logger.Information($"{GetType().Name} (NotifyAsync): Notification complete.");
            return result;
        }
        
        private (int, string) CurrentRange<T>((int, string) currentRange, T componentType) where T : class
        {
            // Casting to expected type.
            var specificType = componentType as Football;

            // Contract requirements. Duplicating the validation here. Should we?
            ValidationConfirmation(specificType);

            var pointDifference = specificType.CalculatePointDifference();
            _logger.Debug($"{GetType().Name} (NotifyAsync): Point difference calculated: {pointDifference}.");

            currentRange = EvaluateData(currentRange, pointDifference, specificType);

            return currentRange;
        }

        private static (int, string) EvaluateData((int, string) currentRange, int range, Football specificType)
        {
            if (range < currentRange.Item1)
            {
                currentRange.Item1 = range;
                currentRange.Item2 = specificType.TeamName;
            }

            return currentRange;
        }

        private static void ValidationConfirmation(Football football)
        {
            var footballValidationResult = football.IsValid(new FootballValidator());
            if (!footballValidationResult.IsValid)
            {
                throw new ArgumentException(footballValidationResult.Errors.Select(m => m.ErrorMessage).ToString());
            }
        }
    }
}
