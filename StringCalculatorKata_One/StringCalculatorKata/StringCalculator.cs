using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace StringCalculatorKata
{
    public class StringCalculator
    {
        private readonly List<string> _delimeters;
 
        public StringCalculator()
        {
            _delimeters = new List<string>
            {
                ",",
                "\n",
                ";",
                ":"
            };
        }


        public string Add()
        {
            return "0";
        }

        public string Add(string value)
        {
            var values = new List<string>();

            foreach (var character in value)
            {
                if (!_delimeters.Contains(character.ToString(CultureInfo.InvariantCulture)) && character != ' ')
                {
                    values.Add(character.ToString(CultureInfo.InvariantCulture));
                }

                if (character == '-')
                {
                    throw new ArgumentOutOfRangeException(String.Format("No negative values allowed.  Values: {0}", value));
                }
            }

            var numbers = new List<int>();

            foreach (var number in values)
            {
                int tempNumber;
                Int32.TryParse(number, out tempNumber);

                numbers.Add(tempNumber);
            }

            int result = numbers.Aggregate(0, (current, figure) => current + figure);

            return result.ToString(CultureInfo.InvariantCulture);
        }

        
    }
}
