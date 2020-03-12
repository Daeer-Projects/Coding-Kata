using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;

namespace Calculator
{
    public static class CalculatorExtensions
    {
        private static char[] InvalidCharacters = new[] {'`', '¬', '!', '"', '£', '$'};

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

            if (ValidateInput(input))
            {             
                // We need an array of the items, minus any spaces.
                var array = input.ToCharArray();
                var strippedArray = array.Where(c => !string.IsNullOrWhiteSpace(c.ToString())).Select(c => c).ToArray();

                var sortedList = SortList(strippedArray);
                var divisionProcess = ProcessDivision(sortedList);
                var multiplicationProcess = ProcessMultiplication(divisionProcess);
                var additionProcess = ProcessAddition(multiplicationProcess);
                var subtractionProcess = ProcessSubtraction(additionProcess);

                result = subtractionProcess.First();
            }
            else
            {
                result = "0";
            }

            return result;
        }

        private static bool ValidateInput(string input)
        {
            var result = true;

            if (!IsNullOrWhiteSpace(input))
            {
                if (ContainsNonCalculationItems(input))
                {
                    result = false;
                }
            }
            else
            {
                result = false;
            }

            return result;
        }

        private static bool IsNullOrWhiteSpace(string input)
        {
            return string.IsNullOrWhiteSpace(input);
        }

        private static bool ContainsNonCalculationItems(string input)
        {
            var result = Regex.Match(input, @"^[0-9+\-*\/\(\)\s]*$");
            return !result.Success;
        }

        private static List<string> SortList(this char[] inputArray)
        {
            var sortedList = new List<string>();

            if (inputArray.Length > 1)
            {
                for (var i = 0; i < inputArray.Length; i++)
                {
                    if (i != inputArray.Length && char.IsDigit(inputArray[i]))
                    {
                        if (i + 1 < inputArray.Length && char.IsDigit(inputArray[i + 1]))
                        {
                            sortedList.Add(inputArray[i].ToString() + inputArray[i + 1]);
                        }
                        else
                        {
                            sortedList.Add(inputArray[i].ToString());
                        }
                    }
                    else
                    {
                        sortedList.Add(inputArray[i].ToString());
                    }
                }
            }
            else
            {
                sortedList.Add(inputArray[0].ToString());
            }

            return sortedList;
        }

        private static List<string> ProcessDivision(this IReadOnlyList<string> input)
        {
            var processed = new List<string>();

            if (input.Count > 1)
            {
                for (var i = 1; i <= input.Count - 1; i++)
                {
                    if (input[i] == "/")
                    {
                        var leftValue = float.Parse(input[i - 1]);
                        var rightValue = float.Parse(input[i + 1]);

                        processed.Add(leftValue == 0 ? rightValue.ToString(CultureInfo.InvariantCulture) :
                            rightValue == 0 ? leftValue.ToString(CultureInfo.InvariantCulture) :
                            (leftValue / rightValue).ToString(CultureInfo.InvariantCulture));
                        i++;
                    }
                    else
                    {
                        if (i == 1)
                        {
                            processed.Add(input[i - 1]);
                            processed.Add(input[i]);
                        }
                        else if (i == input.Count - 1)
                        {
                            processed.Add(input[i]);
                            processed.Add(input[i + 1]);
                        }
                        else
                        {
                            processed.Add(input[i]);
                        }
                    }
                }
            }
            else
            {
                processed.Add(input[0]);
            }

            return processed;
        }

        private static List<string> ProcessMultiplication(this IReadOnlyList<string> input)
        {
            var processed = new List<string>();

            if (input.Count > 1)
            {
                for (var i = 1; i <= input.Count - 1; i++)
                {
                    if (input[i] == "*")
                    {
                        var leftValue = float.Parse(input[i - 1]);
                        var rightValue = float.Parse(input[i + 1]);

                        processed.Add((leftValue * rightValue).ToString(CultureInfo.InvariantCulture));
                        i++;
                    }
                    else
                    {
                        if (i == 1)
                        {
                            processed.Add(input[i - 1]);
                            processed.Add(input[i]);
                        }
                        else if (i == input.Count - 1)
                        {
                            processed.Add(input[i]);
                            processed.Add(input[i + 1]);
                        }
                        else
                        {
                            processed.Add(input[i]);
                        }
                    }
                }
            }
            else
            {
                processed.Add(input[0]);
            }

            return processed;
        }

        private static List<string> ProcessAddition(this IReadOnlyList<string> input)
        {
            var processed = new List<string>();

            if (input.Count > 1)
            {
                for (var i = 1; i <= input.Count - 1; i++)
                {
                    if (input[i] == "+")
                    {
                        var leftValue = float.Parse(input[i - 1]);
                        var rightValue = float.Parse(input[i + 1]);

                        processed.Add((leftValue + rightValue).ToString(CultureInfo.InvariantCulture));
                        i++;
                    }
                    else
                    {
                        if (i == 1)
                        {
                            processed.Add(input[i - 1]);
                            processed.Add(input[i]);
                        }
                        else if (i == input.Count - 1)
                        {
                            processed.Add(input[i]);
                            processed.Add(input[i + 1]);
                        }
                        else
                        {
                            processed.Add(input[i]);
                        }
                    }
                }
            }
            else
            {
                processed.Add(input[0]);
            }

            return processed;
        }

        private static List<string> ProcessSubtraction(this IReadOnlyList<string> input)
        {
            var processed = new List<string>();

            if (input.Count > 1)
            {
                for (var i = 1; i <= input.Count - 1; i++)
                {
                    if (input[i] == "-")
                    {
                        var leftValue = float.Parse(input[i - 1]);
                        var rightValue = float.Parse(input[i + 1]);

                        processed.Add((leftValue - rightValue).ToString(CultureInfo.InvariantCulture));
                        i++;
                    }
                    else
                    {
                        if (i == 1)
                        {
                            processed.Add(input[i - 1]);
                            processed.Add(input[i]);
                        }
                        else if (i == input.Count - 1)
                        {
                            processed.Add(input[i]);
                            processed.Add(input[i + 1]);
                        }
                        else
                        {
                            processed.Add(input[i]);
                        }
                    }
                }
            }
            else
            {
                processed.Add(input[0]);
            }

            return processed;
        }
    }
}
