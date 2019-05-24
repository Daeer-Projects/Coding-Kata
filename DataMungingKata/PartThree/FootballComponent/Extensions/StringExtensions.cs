using System;
using System.Collections.Generic;

using FootballComponent.Configuration;
using FootballComponent.Types;

namespace FootballComponent.Extensions
{
    /// <summary>
    /// Extension methods to extract football information out of a string.
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// The main extension method that takes a string and returns a football object.
        /// </summary>
        /// <param name="item"> The string we are processing. </param>
        /// <returns> The football validator type that contains a football object if validation passed. </returns>
        public static FootballValidatorType ToFootball(this string item)
        {
            var isFootballValidType = new FootballValidatorType();

            var (canExtract, errorMessage, data) = CanExtractFootballItems(item);
            if (canExtract)
            {
                isFootballValidType.ExtractFootballItems(data);
            }
            else
            {
                isFootballValidType.IsValid = false;
                isFootballValidType.ErrorList.Add(errorMessage);
            }

            return isFootballValidType;
        }

        private static (bool, string, List<string>) CanExtractFootballItems(string item)
        {
            var results = (canExtract: false, errorMessage: string.Empty, dataList: new List<string>());

            try
            {
                results.dataList.Add(item.Substring(FootballConfig.TeamColumnStart, FootballConfig.TeamColumnLength));
                results.dataList.Add(item.Substring(FootballConfig.ForColumnStart, FootballConfig.ForColumnLength));
                results.dataList.Add(item.Substring(FootballConfig.AgainstColumnStart, FootballConfig.AgainstColumnLength));
                results.canExtract = true;
            }
            catch (ArgumentOutOfRangeException exception)
            {
                results.canExtract = false;
                results.errorMessage = $"Exception raised: {exception.Message}.";
            }

            return results;
        }

        private static void ExtractFootballItems(this FootballValidatorType isFootballValidType, IReadOnlyList<string> data)
        {
            var team = data[0];
            var forPoints = data[1];
            var againstPoints = data[2];

            if (int.TryParse(forPoints, out var forAsInt) && int.TryParse(againstPoints, out var againstAsInt))
            {
                var result = new Football
                {
                    TeamName = team.Trim(),
                    ForPoints = forAsInt,
                    AgainstPoints = againstAsInt
                };

                isFootballValidType.ProcessFootballValidation(result);
            }
            else
            {
                isFootballValidType.IsValid = false;
                isFootballValidType.ErrorList.Add($"Invalid items. Team: {team}, For Points: {forPoints}, Against Points: {againstPoints}.");
            }
        }

        private static void ProcessFootballValidation(this FootballValidatorType isFootballValidType, Football result)
        {
            var validationResult = result.IsValid();
            if (validationResult.IsValid)
            {
                isFootballValidType.IsValid = true;
                isFootballValidType.Football = result;
            }
            else
            {
                isFootballValidType.IsValid = false;
                isFootballValidType.ErrorList.Add($"Invalid Football.");

                foreach (var validationResultError in validationResult.Errors)
                {
                    isFootballValidType.ErrorList.Add(validationResultError.ErrorMessage);
                }
            }
        }
    }
}
