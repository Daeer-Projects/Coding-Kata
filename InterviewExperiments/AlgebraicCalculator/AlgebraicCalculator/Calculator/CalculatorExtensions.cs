using System;
using System.Linq;

namespace Calculator
{
    public static class CalculatorExtensions
    {


        /// <summary>
        /// Implement a basic algebraic calculator that takes an expression string as input and provides
        /// the result as output.
        ///
        /// The expression string may contain:
        /// * Plus: +
        /// * Minus: –
        /// * Multiplication: *
        /// * Division: /
        /// * Non-negative integers
        /// * Empty spaces
        ///
        /// Note:
        /// * Use any high-level programming language
        /// * Do not use built-in eval() library functions
        /// * Outputs should be shown as a fraction if there are decimals
        /// </summary>
        /// <param name="input"> The input that we are calculating the result from. </param>
        /// <returns>
        /// The final result.  If the result is a decimal, we are rendering them as a fraction.
        /// </returns>
        public static string CalculateValue(this string input)
        {
            var result = string.Empty;

            // We need an array of the items, minus any spaces.
            var array = input.ToCharArray();
            var strippedArray = input.Where(c => !string.IsNullOrWhiteSpace(c.ToString())).Select(c => c);

            // How can I go through the stripped array and make a calculation based on the digits and function?
            float sum = 0f;

            foreach (var item in strippedArray)
            {
                // I don't think this is going to be possible.
            }


            return result;
        }
    }
}
