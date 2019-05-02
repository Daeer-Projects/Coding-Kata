using System;
using System.IO.Abstractions;
using DataMungingPartTwo.Constants;
using DataMungingPartTwo.Processors;

namespace DataMungingPartTwo
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello.");

            var reader = new FileReader(new FileSystem());
            var football = new FootballNotifier();
            var processor = new TeamProcessor(reader, football);

            Console.WriteLine($"Processing the file '{AppConstants.FullFileName}'.");
            try
            {
                var result = processor.GetTeamWithLeastPointDifference(AppConstants.FullFileName);

                Console.WriteLine($"The result is: {result}.");
            }
            catch (Exception exception)
            {
                Console.WriteLine($"The application threw the following exception: {exception.Message}.");
            }

            Console.WriteLine("I hoped you enjoyed it!");
        }
    }
}
