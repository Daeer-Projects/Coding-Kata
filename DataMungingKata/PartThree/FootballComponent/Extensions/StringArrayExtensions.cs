using FootballComponent.Validators;

namespace FootballComponent.Extensions
{
    /// <summary>
    /// Extensions for string arrays used by the football component.
    /// </summary>
    public static class StringArrayExtensions
    {
        /// <summary>
        /// Checks the data array to ensure it is valid for processing.
        /// </summary>
        /// <param name="data"> The collection of data rows. </param>
        /// <returns> If the data is valid or not. </returns>
        public static bool IsValid(this string[] data)
        {
            var validator = new StringArrayValidator();

            var result = validator.Validate(data);

            return result.IsValid;
        }
    }
}
