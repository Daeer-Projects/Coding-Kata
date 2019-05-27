using FluentValidation;
using FluentValidation.Results;

namespace DataMungingCoreV2.Extensions
{
    public static class ComponentExtensions
    {
        public static ValidationResult IsValid<T, TV>(this T component)
            where T: class
            where TV: AbstractValidator<T>, new()
        {
            var validator = new TV();

            var result = validator.Validate(component);

            return result;
        }
    }
}
