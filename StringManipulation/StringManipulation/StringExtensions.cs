using System.Linq;

namespace StringManipulation
{
    /// <summary>
    /// A collection of string extensions.
    /// This is taken from the test example I was asked during my interview.
    ///
    /// Given a string and an optional count value, can you process the input string
    /// and output a single (unless defined) character for each of the characters
    /// in the input string.
    ///
    /// Example:
    /// "AASDEDFFRDSSSSEDSW" using the default count of 1, should return "ASDEDFRDSEDSW"
    /// "AAAASDEEEERFFDRETYTGGFDDD" using a count of 2, should return "AASDEERFFDRETYTGGFDD"
    /// "AAAASDEEEERFFDRETYTGGFDDD" using a count of 3, should return "AAASDEEERFFDRETYTGGFDDD"
    /// </summary>
    public static class StringExtensions
    {
        //// Attempt 1: Not very elegant, but it works.

        ///// <summary>
        ///// A way to extract the characters from an input into the required format.
        ///// </summary>
        ///// <param name="inputString"> The input that we are processing. </param>
        ///// <param name="itemCount"> The count of characters we can accept before we ignore the character. </param>
        ///// <returns>
        ///// The processed string.
        ///// </returns>
        //public static string Manipulate(this string inputString, int itemCount = 1)
        //{
        //    var result = string.Empty;
        //    var count = 1;

        //    foreach (var element in inputString)
        //    {
        //        if (!result.Any())
        //        {
        //            // The first item in the input string.
        //            result = element.ToString();
        //        }
        //        else if (result.Last() == element)
        //        {
        //            // The element is the same as the last item in result.
        //            // We want to count how many times we get in here.
        //            // But if the count is less than what we have defined, then we want to add
        //            // the element to the result.
        //            if (count < itemCount)
        //            {
        //                result += element;
        //            }
        //            count++;
        //        }
        //        else
        //        {
        //            // Not the first item in the input.
        //            // It's not the same as the last item in the result.
        //            // So add it to the result, and reset the count.
        //            result += element;
        //            count = 1;
        //        }
        //    }

        //    return result;
        //}

        // Attempt 2: Not very elegant, but it works.

        /// <summary>
        /// A way to extract the characters from an input into the required format.
        /// </summary>
        /// <param name="inputString"> The input that we are processing. </param>
        /// <param name="itemCount"> The count of characters we can accept before we ignore the character. </param>
        /// <returns>
        /// The processed string.
        /// </returns>
        public static string Manipulate(this string inputString, int itemCount = 1)
        {
            var result = string.Empty;
            var count = 1;

            foreach (var element in inputString)
            {
                if (result.Any() && result.Last() == element)
                {
                    if (count < itemCount)
                    {
                        result += element;
                    }
                    count++;
                }
                else
                {
                    result += element;
                    count = 1;
                }
            }

            return result;
        }

    }
}
