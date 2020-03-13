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
            var result = "0";

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
                    if (double.TryParse(additionAndSubtraction.First(), out var decimalVersion))
                    {
                        result = decimalVersion.ToFraction();
                    }
                }
                else
                {
                    result = additionAndSubtraction.First();
                }
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
                for (var index = 0; index < inputArray.Length; index++)
                {
                    if (index != inputArray.Length && char.IsDigit(inputArray[index]))
                    {
                        if (index + 1 < inputArray.Length && char.IsDigit(inputArray[index + 1]))
                        {
                            sortedList.Add(inputArray[index].ToString() + inputArray[index + 1]);
                        }
                        else
                        {
                            sortedList.Add(inputArray[index].ToString());
                        }
                    }
                    else
                    {
                        sortedList.Add(inputArray[index].ToString());
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
                for (var index = 1; index < input.Count; index++)
                {
                    switch (input[index])
                    {
                        case "/":
                            {
                                processed = ProcessDivision(input, processed, index);
                                index++;
                                break;
                            }
                        case "*":
                            {
                                processed = ProcessMultiplication(input, processed, index);
                                index++;
                                break;
                            }
                        default:
                            {
                                StandardAddToProcessed(input, processed, index);
                                break;
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
                for (var index = 1; index < input.Count; index++)
                {
                    switch (input[index])
                    {
                        case "+":
                            {
                                processed = ProcessAddition(input, processed, index);
                                index++;
                                break;
                            }
                        case "-":
                            {
                                processed = ProcessSubtraction(input, processed, index);
                                index++;
                                break;
                            }
                        default:
                            {
                                StandardAddToProcessed(input, processed, index);
                                break;
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

        private static List<string> ProcessDivision(IReadOnlyList<string> input, List<string> processed, int index)
        {
            var result = ProcessCalculation(input, processed, index, PerformDivisionCalculation);
            return result;
        }

        private static List<string> ProcessMultiplication(IReadOnlyList<string> input, List<string> processed, int index)
        {
            var result = ProcessCalculation(input, processed, index, PerformMultiplicationCalculation);
            return result;
        }

        private static List<string> ProcessAddition(IReadOnlyList<string> input, List<string> processed, int index)
        {
            var result = ProcessCalculation(input, processed, index, PerformAdditionCalculation);
            return result;
        }

        private static List<string> ProcessSubtraction(IReadOnlyList<string> input, List<string> processed, int index)
        {
            var result = ProcessCalculation(input, processed, index, PerformSubtractionCalculation);
            return result;
        }

        private static List<string> ProcessCalculation(IReadOnlyList<string> input, List<string> processed, int index,
            Func<double, double, string> performCalculation)
        {
            var leftValue = GetLeftValue(input, processed, index);
            var rightValue = GetRightValue(input, index);
            var calculation = performCalculation(leftValue, rightValue);

            if (processed.Any())
            {
                var tempList = DefineTempList(processed, calculation);
                processed = tempList;
            }
            else
            {
                processed.Add(calculation);
            }

            return processed;
        }

        private static void StandardAddToProcessed(IReadOnlyList<string> input, ICollection<string> processed, int index)
        {
            if (index == 1)
            {
                processed.Add(input[index - 1]);
                processed.Add(input[index]);
            }
            else
            {
                processed.Add(input[index]);
            }
        }

        private static double GetLeftValue(IReadOnlyList<string> input, List<string> processed, int index)
        {
            return processed.Any() ? double.Parse(processed.Last()) : double.Parse(input[index - 1]);
        }

        private static double GetRightValue(IReadOnlyList<string> input, int i)
        {
            return double.Parse(input[i + 1]);
        }

        private static string PerformDivisionCalculation(double leftValue, double rightValue)
        {
            var calculation = GetDivisionCalculation(leftValue, rightValue, (leftValue / rightValue).ToString(CultureInfo.InvariantCulture));
            return calculation;
        }

        private static string PerformMultiplicationCalculation(double leftValue, double rightValue)
        {
            var calculation = (leftValue * rightValue).ToString(CultureInfo.InvariantCulture);
            return calculation;
        }

        private static string PerformAdditionCalculation(double leftValue, double rightValue)
        {
            var calculation = (leftValue + rightValue).ToString(CultureInfo.InvariantCulture);
            return calculation;
        }

        private static string PerformSubtractionCalculation(double leftValue, double rightValue)
        {
            var calculation = (leftValue - rightValue).ToString(CultureInfo.InvariantCulture);
            return calculation;
        }

        private static string GetDivisionCalculation(double leftValue, double rightValue, string calculation)
        {
            return (leftValue.Equals(0) ? rightValue.ToString(CultureInfo.InvariantCulture) :
                rightValue.Equals(0) ? leftValue.ToString(CultureInfo.InvariantCulture) : calculation);
        }

        private static List<string> DefineTempList(List<string> processed, string calculation)
        {
            var tempList = processed.Select(element => element == processed.Last()
                    ? calculation
                    : element)
                .ToList();
            return tempList;
        }
    }
}
