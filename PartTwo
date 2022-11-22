using System;
using System.Collections.Generic;
using System.Linq;

namespace kata04.data.munging
{
    public class Parser
    {
        public static List<Row> ParseWeather(string input)
        {
            return GenericParse(
                input: input,
                skipBeginning: 2, // skip header + empty row
                skipEnd: 2, // skip total row + empty line
                name: Range.New(2, 2), // day name is a 2-digit number
                a: Range.New(6, 2), // temp starts at character index 6 and is 2 digits
                b: Range.New(12, 2)); // temp starts at character index 12 and is 2 digits
        }

        public static List<Row> ParseFootball(string input)
        {
            return GenericParse(input, 1, 1, Range.New(7, 15), Range.New(43, 2), Range.New(50, 2));
        }

        private static List<Row> GenericParse(string input, int skipBeginning, int skipEnd, Range name, Range a, Range b)
        {
            string[] split = input.Split("\n");
            var parsed = split
                .Skip(skipBeginning)
                .Take(split.Length - (skipBeginning + skipEnd))
                .Where(row => !row.Contains("-------------------------------------------------------"))
                .Select(x =>
                {
                    return new Row()
                    {
                        Name = x.Substring(name.From, name.Length).Trim(),
                        ValueA = int.Parse(x.Substring(a.From, a.Length)),
                        ValueB = int.Parse(x.Substring(b.From, b.Length))
                    };
                })
                .ToList();
            return parsed;
        }

        public class Range
        {
            public int From;
            public int Length;

            public static Range New(int from, int length)
            {
                return new Range{From = from, Length = length};
            }
        }

        public class Row
        {
            public string Name;
            public int ValueA;
            public int ValueB;

            public int AbsDiff
            {
                get
                {
                    return Math.Abs(ValueB - ValueA);
                }
            }
        }
    }
}
