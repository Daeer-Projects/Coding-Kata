using System.Linq;

using FluentValidation;

namespace DataMungingCoreV2.Validators
{
    public class BaseStringArrayValidator : AbstractValidator<string[]>
    {
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
