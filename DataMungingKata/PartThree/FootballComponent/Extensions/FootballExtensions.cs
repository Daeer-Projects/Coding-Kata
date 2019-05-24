using System;

using FluentValidation.Results;
using FootballComponent.Types;
using FootballComponent.Validators;

namespace FootballComponent.Extensions
{
    /// <summary>
    /// Some extension methods used in the football component.
    /// </summary>
    public static class FootballExtensions
    {
        /// <summary>
        /// Checks if the football object is valid or not.
        /// </summary>
        /// <param name="football"> The football object we are validating. </param>
        /// <returns> If the football object is valid or not. </returns>
        public static ValidationResult IsValid(this Football football)
        {
            var validator = new FootballValidator();

            var result = validator.Validate(football);

            return result;
        }

        /// <summary>
        /// A simple calculation to identify the point difference.
        /// </summary>
        /// <param name="football"> The football object we are using for the calculation. </param>
        /// <returns> The points difference. </returns>
        public static int CalculatePointDifference(this Football football)
        {
            return Math.Abs(football.ForPoints - football.AgainstPoints);
        }
    }
}
