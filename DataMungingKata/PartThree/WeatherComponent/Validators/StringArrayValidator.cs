using System;
using System.Linq;

using FluentValidation;
using WeatherComponent.Constants;

namespace WeatherComponent.Validators
{
    public class StringArrayValidator : AbstractValidator<string[]>
    {
        public StringArrayValidator()
        {
            RuleFor(data => data).NotNull();
            RuleFor(data => data).NotEmpty();
            RuleFor(data => data.Length).GreaterThan(0);
            RuleFor(data => data).Must(HeaderShouldMatch);
            RuleFor(data => data[1]).Equals(string.Empty);
            RuleFor(data => data).Must(LastRowShouldContain);
            RuleFor(data => data).Must(MustContainDataRows);
        }

        private bool HeaderShouldMatch(string[] data)
        {
            bool result;
            try
            {
                result = data.First().Equals(WeatherConstants.WeatherHeader);
            }
            catch (ArgumentNullException)
            {
                result = false;
            }
            catch (InvalidOperationException)
            {
                result = false;
            }

            return result;
        }

        private bool LastRowShouldContain(string[] data)
        {
            bool result;
            try
            {
                result = data.Last().Contains(WeatherConstants.WeatherLastRowFirstColumn);
            }
            catch (ArgumentNullException)
            {
                result = false;
            }
            catch (InvalidOperationException)
            {
                result = false;
            }

            return result;
        }

        private bool MustContainDataRows(string[] data)
        {
            var countOfDataRows = data.Count(item =>
                !item.Contains(WeatherConstants.WeatherHeader) && 
                !string.IsNullOrWhiteSpace(item) &&
                !item.Contains(WeatherConstants.WeatherLastRowFirstColumn));

            return countOfDataRows > 0;
        }
    }
}
