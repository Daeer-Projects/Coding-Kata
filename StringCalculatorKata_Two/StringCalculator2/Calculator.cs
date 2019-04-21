using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StringCalculator2
{
    public class Calculator : ICalculator
    {
        private static char DefaultDeliminator { get; set; }
        private static StringBuilder ErrorMessage { get; set; }
        private static IEnumerable<int> PositiveNumbers { get; set; }
        private static List<char> Deliminators { get; set; }
        private static IEnumerable<string> StringNumbers { get; set; }
        private static IEnumerable<int> IntNumbers { get; set; }
        
        public Calculator()
        {
            DefaultDeliminator = ',';
            ErrorMessage = new StringBuilder();
            Deliminators = new List<char>{'\n'};
        }

        public int Add(string numbers)
        {
            PositiveNumbers = GetNumbersAsInts(numbers);

            int result = CalculateTheResult();

            return result;
        }

        private static IEnumerable<int> GetNumbersAsInts(string numbers)
        {
            CheckNumbersForNewDeliminator(numbers);

            numbers = ConvertNewLinesToDefaultDeliminator(numbers);

            StringNumbers = SplitNumbers(numbers);
            
            IntNumbers = PutNumbersIntoIntList();

            if (ErrorMessage.Length > 0 )
            {
                throw new ArgumentOutOfRangeException(ErrorMessage.ToString());
            }
            return IntNumbers;
        }

        private static void CheckNumbersForNewDeliminator(string numbers)
        {
            if (numbers != null && numbers.Count() > 2 && numbers.StartsWith("//"))
            {
                Deliminators.Add(numbers[2]);
            }
        }

        private static string ConvertNewLinesToDefaultDeliminator(string numbers)
        {
            return Deliminators.Aggregate(numbers, (current, deliminator) => current.Replace(deliminator, DefaultDeliminator));
        }

        private static IEnumerable<string> SplitNumbers(string numbers)
        {
            var stringNumbers = numbers.Split(DefaultDeliminator).ToList();
            return stringNumbers;
        }

        private static IEnumerable<int> PutNumbersIntoIntList()
        {
            var results = new List<int>();
            foreach (var stringNumber in StringNumbers)
            {
                int temp;
                Int32.TryParse(stringNumber, out temp);

                temp = CheckForNegativesAndBigNumbers(temp);

                results.Add(temp);
            }
            return results;
        }

        private static int CheckForNegativesAndBigNumbers(int temp)
        {
            if (temp < 0)
            {
                ErrorMessage.AppendLine(string.Format("Negatives are not allowed: {0}.", temp));
            }
            else if (temp > 1000)
            {
                temp = 0;
            }
            return temp;
        }

        private static int CalculateTheResult()
        {
            return PositiveNumbers.Aggregate(0, (current, positiveNumber) => current + positiveNumber);
        }
    }
}
