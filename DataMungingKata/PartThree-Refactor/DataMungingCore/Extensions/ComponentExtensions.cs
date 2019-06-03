using FluentValidation;
using FluentValidation.Results;

namespace DataMungingCoreV2.Extensions
{
    /// <summary>
    /// Extension methods for the components.
    /// </summary>
    public static class ComponentExtensions
    {
        /// <summary>
        /// Runs the validator against the component.
        /// </summary>
        /// <typeparam name="T"> The type of component to validate. </typeparam>
        /// <param name="component"> The component or type we are going to validate. </param>
        /// <param name="validator"> The supplied validator for the component or type. </param>
        /// <returns>
        /// The FluentValidation ValidationResult object returned from the validator.
        /// This contains the flag of IsValid and a list of error messages.
        /// </returns>
        public static ValidationResult IsValid<T>(this T component, AbstractValidator<T> validator)
            where T : class
        {
            var result = validator.Validate(component);

            return result;
        }
    }
}
