using System;
using System.Collections.Generic;
using System.IO.Abstractions;
using System.Text;
using System.Threading.Tasks;
using WeatherComponent.Constants;
using WeatherComponent.Processors;

namespace DataMungingPartThree
{
    public class Bootstrapper
    {
        public void ProcessItems()
        {
            var reader = new WeatherReader(new FileSystem());
            var mapper = new WeatherMapper();
            var notifier = new WeatherNotifier();
            var processor = new WeatherProcessor(reader, mapper, notifier);

            Console.WriteLine($"Processing the file '{WeatherConstants.FullFileName}'.");
            try
            {
                 var result = processor.ProcessAsync(WeatherConstants.FullFileName);

                Console.WriteLine($"The result is: {result.Result}.");
            }
            catch (Exception exception)
            {
                Console.WriteLine($"The application threw the following exception: {exception.Message}.");
            }
        }
    }
}
