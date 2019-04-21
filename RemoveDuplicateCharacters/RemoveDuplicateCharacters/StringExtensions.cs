using System;
using System.Collections.Generic;

namespace RemoveDuplicateCharacters
{
    public static class StringExtensions
    {
        /// <summary>
        /// Removes duplicate characters from a given string if they exceed the allowed number of duplicates.
        /// This is a different version from the C#, as we now have some different requirements.
        /// Examples:
        /// RemoveDuplicateCharacters("AAABCDDDBCBC", 0) returns "ABCD"
        /// RemoveDuplicateCharacters("AAABCDDDBCBC", 1) returns "AABCDDBC"
        /// RemoveDuplicateCharacters("AAABCDDDBCBC", 2) returns "AAABCDDDBCBC"
        /// </summary>
        /// <param name="originalString"> String to remove duplicates from. </param>
        /// <param name="allowedDuplicates"> Maximum number of duplicates of each character in the resulting string. </param>
        /// <exception cref="ArgumentNullException"> originalString is null or empty. </exception>
        /// <exception cref="ArgumentOutOfRangeException"> allowedDuplicates is less than zero. </exception>
        /// <returns>
        /// A string containing only the maximum number of character duplicates for each character in the string.
        /// </returns>
        public static string RemoveDuplicateCharacters(this string originalString, int allowedDuplicates = 0)
        {
            // Contract requirements.
            if (string.IsNullOrEmpty(originalString)) throw new ArgumentNullException(originalString, "Input string must contain something.");
            if (allowedDuplicates < 0) throw new ArgumentOutOfRangeException(nameof(allowedDuplicates), "Allowed duplicates must be greater than zero.");

            var result = string.Empty;

            // As the allowed duplicates can be converted to the total count of the character, lets set that now.
            var totalAllowedCharacters = ++allowedDuplicates;
            var characterCounts = new Dictionary<char, int>();

            // Lets go through the list of characters.
            foreach (var currentCharacter in originalString)
            {
                var characterCount = 1;

                if (characterCounts.ContainsKey(currentCharacter))
                {
                    characterCount = characterCounts.GetValueOrDefault(currentCharacter);
                    characterCounts[currentCharacter] = ++characterCount;
                }
                else
                {
                    characterCounts.Add(currentCharacter, characterCount);
                }

                // Has the current character reached the limit?
                result = characterCount <= totalAllowedCharacters ? result + currentCharacter : result;
            }

            return result;
        }
    }
}
