using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;

namespace Calculator
{
    public static class CalculatorExtensions
    {
        private static char[] InvalidCharacters = new[] { '`', '¬', '!', '"', '£', '$' };

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
                var divisionAndMultiplication = ProcessDivisionAndMultiplication(sortedList);
                var additionAndSubtraction = ProcessAdditionAndSubtraction(divisionAndMultiplication);

                if (additionAndSubtraction.First().Contains('.'))
                {
                    var decimalVersion = double.Parse(additionAndSubtraction.First());
                    result = decimalVersion.ToFraction();
                }
                else
                {
                    result = additionAndSubtraction.First();
                }
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

        /// <summary>
        /// I don't like this.  What if there is a three digit value?  I am only
        /// accounting for two digits here!
        /// </summary>
        /// <param name="inputArray"></param>
        /// <returns></returns>
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

        private static List<string> ProcessDivisionAndMultiplication(this IReadOnlyList<string> input)
        {
            var processed = new List<string>();

            if (input.Count > 1)
            {
                for (var i = 1; i < input.Count; i++)
                {
                    if (input[i] == "/")
                    {
                        var leftValue = processed.Any() ? double.Parse(processed.Last()) : double.Parse(input[i - 1]);
                        var rightValue = double.Parse(input[i + 1]);
                        var calculation = (leftValue / rightValue).ToString(CultureInfo.InvariantCulture);

                        if (processed.Any())
                        {
                            var tempList = processed.Select(element => element == processed.Last()
                                    ? (leftValue.Equals(0) ? rightValue.ToString(CultureInfo.InvariantCulture) :
                                        rightValue.Equals(0) ? leftValue.ToString(CultureInfo.InvariantCulture) :
                                        calculation)
                                    : element)
                                .ToList();

                            processed = tempList;
                        }
                        else
                        {
                            processed.Add(leftValue.Equals(0) ? rightValue.ToString(CultureInfo.InvariantCulture) :
                                rightValue.Equals(0) ? leftValue.ToString(CultureInfo.InvariantCulture) :
                                calculation);
                        }

                        i++;
                    }
                    else if (input[i] == "*")
                    {
                        var leftValue = processed.Any() ? double.Parse(processed.Last()) : double.Parse(input[i - 1]);
                        var rightValue = double.Parse(input[i + 1]);
                        var calculation = (leftValue * rightValue).ToString(CultureInfo.InvariantCulture);

                        if (processed.Any())
                        {
                            var tempList = processed.Select(element => element == processed.Last()
                                    ? calculation
                                    : element)
                                .ToList();

                            processed = tempList;
                        }
                        else
                        {
                            processed.Add(calculation);
                        }

                        i++;
                    }
                    else
                    {
                        if (i == 1)
                        {
                            processed.Add(input[i - 1]);
                            processed.Add(input[i]);
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

        private static List<string> ProcessAdditionAndSubtraction(this IReadOnlyList<string> input)
        {
            var processed = new List<string>();

            if (input.Count > 1)
            {
                for (var i = 1; i < input.Count; i++)
                {
                    if (input[i] == "+")
                    {
                        var leftValue = processed.Any() ? double.Parse(processed.Last()) : double.Parse(input[i - 1]);
                        var rightValue = double.Parse(input[i + 1]);
                        var calculation = (leftValue + rightValue).ToString(CultureInfo.InvariantCulture);

                        if (processed.Any())
                        {
                            var tempList = processed.Select(element => element == processed.Last()
                                    ? calculation
                                    : element)
                                .ToList();

                            processed = tempList;
                        }
                        else
                        {
                            processed.Add(calculation);
                        }

                        i++;
                    }
                    else if (input[i] == "-")
                    {
                        var leftValue = processed.Any() ? double.Parse(processed.Last()) : double.Parse(input[i - 1]);
                        var rightValue = double.Parse(input[i + 1]);
                        var calculation = (leftValue - rightValue).ToString(CultureInfo.InvariantCulture);

                        if (processed.Any())
                        {
                            var tempList = processed.Select(element => element == processed.Last()
                                    ? calculation
                                    : element)
                                .ToList();

                            processed = tempList;
                        }
                        else
                        {
                            processed.Add(calculation);
                        }

                        i++;
                    }
                    else
                    {
                        if (i == 1)
                        {
                            processed.Add(input[i - 1]);
                            processed.Add(input[i]);
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
