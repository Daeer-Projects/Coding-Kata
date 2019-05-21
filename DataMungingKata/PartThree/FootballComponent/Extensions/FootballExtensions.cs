using System;
using FluentValidation.Results;
using FootballComponent.Types;
using FootballComponent.Validators;

namespace FootballComponent.Extensions
{
    public static class FootballExtensions
    {
        public static ValidationResult IsValid(this Football football)
        {
            var validator = new FootballValidator();

            var result = validator.Validate(football);

            return result;
        }

        public static int CalculatePointDifference(this Football football)
        {
            return Math.Abs(football.ForPoints - football.AgainstPoints);
        }
    }
}
