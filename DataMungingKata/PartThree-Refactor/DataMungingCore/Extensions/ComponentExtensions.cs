using FluentValidation;
using FluentValidation.Results;

namespace DataMungingCoreV2.Extensions
{
    public static class ComponentExtensions
    {
        public static ValidationResult IsValid<T>(this T component, AbstractValidator<T> validator)
            where T : class
        {
            var result = validator.Validate(component);

            return result;
        }
    }
}
