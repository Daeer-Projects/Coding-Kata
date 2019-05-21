using System;
using System.Linq;

using FluentValidation;
using FootballComponent.Constants;

namespace FootballComponent.Validators
{
    public class StringArrayValidator : AbstractValidator<string[]>
    {
        public StringArrayValidator()
        {
            RuleFor(data => data).NotNull();
            RuleFor(data => data).NotEmpty();
            RuleFor(data => data.Length).GreaterThan(0);
            RuleFor(data => data).Must(HeaderShouldMatch);
            RuleFor(data => data).Must(MustContainDataRows);
            RuleFor(data => data).Must(MustContainDivider);
            RuleFor(data => data).Must(DividerMustHaveThreeRowsAfterIt);
        }

        private bool HeaderShouldMatch(string[] data)
        {
            bool result;
            try
            {
                result = data.First().Equals(FootballConstants.FootballHeader);
            }
            catch (NullReferenceException)
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
            bool result;

            try
            {
                var countOfDataRows = data.Count(item =>
                    !item.Contains(FootballConstants.FootballHeader) &&
                    !item.Contains(FootballConstants.FootballDivider));

                result = countOfDataRows > 0;
            }
            catch (NullReferenceException)
            {
                result = false;
            }

            return result;
        }

        private bool MustContainDivider(string[] data)
        {
            bool result;

            try
            {
                var countOfDataRows = data.Count(item =>
                    item.Contains(FootballConstants.FootballDivider));

                result = countOfDataRows > 0;
            }
            catch (NullReferenceException)
            {
                result = false;
            }

            return result;
        }

        private bool DividerMustHaveThreeRowsAfterIt(string[] data)
        {
            bool result;

            try
            {
                var indexOfDivider = 0;

                for (var index = 0; index < data.Length; index++)
                {
                    if (data[index].Equals(FootballConstants.FootballDivider))
                    {
                        indexOfDivider = index + 1;
                    }
                }

                result = (data.Length - indexOfDivider) == 3;
            }
            catch (NullReferenceException)
            {
                result = false;
            }

            return result;
        }
    }
}
