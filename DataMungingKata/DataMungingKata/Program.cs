using System;
using System.IO.Abstractions;

using DataMungingKata.Constants;
using DataMungingKata.Processors;

namespace DataMungingKata
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello.");

            var extractor = new FileExtractor(new FileSystem());
            var weather = new WeatherData();
            var manager = new WeatherDataManager(extractor, weather);

            Console.WriteLine($"Processing the file '{AppConstants.FullFileName}'.");
            try
            {
                var result = manager.GetDayWithLeastChange(AppConstants.FullFileName);

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
