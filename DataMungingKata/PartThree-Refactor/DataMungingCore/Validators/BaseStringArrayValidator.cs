using System.Linq;

using FluentValidation;

namespace DataMungingCoreV2.Validators
{
    /// <summary>
    /// A basic validator for a string array.
    /// </summary>
    public class BaseStringArrayValidator : AbstractValidator<string[]>
    {
        /// <summary>
        /// A simple set of validation before thorough validation by the validators in the components.
        /// </summary>
        public BaseStringArrayValidator()
        {
            RuleFor(data => data).NotNull();
            RuleFor(data => data).NotEmpty();
            RuleFor(data => data.Length).GreaterThan(0);
            RuleFor(data => data).Must(DataShouldHaveRows);
            RuleFor(data => data).Must(DataRowsShouldHaveData);
        }

        private bool DataShouldHaveRows(string[] data)
        {
            return data.Any();
        }

        private bool DataRowsShouldHaveData(string[] data)
        {
            var results = true;

            if (data.Any() && data.Length < 2)
            {
                results = !string.IsNullOrWhiteSpace(data.First());
            }
            
            return results;
        }
    }
}
