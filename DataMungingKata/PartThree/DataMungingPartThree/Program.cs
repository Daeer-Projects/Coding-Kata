using System;
using System.IO.Abstractions;
using System.Threading.Tasks;
using WeatherComponent.Constants;
using WeatherComponent.Processors;

namespace DataMungingPartThree
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello.");

            // To get the first component running and ensuring it works correctly, I am just creating things here.
            // ToDo: When we have more components, we need to bootstrap all of the construction.

            // We need to register the components (only one at the moment).
            // We need to set up the event hub.
            // We need to set up the logging for the core, components and program.

            // Just getting it to work for now.  Using the same method I did for part one and two.

            var boot = new Bootstrapper();
            boot.ProcessItems();

            //var reader = new WeatherReader(new FileSystem());
            //var mapper = new WeatherMapper();
            //var notifier = new WeatherNotifier();
            //var processor = new WeatherProcessor(reader, mapper, notifier);

            //Console.WriteLine($"Processing the file '{WeatherConstants.FullFileName}'.");
            //try
            //{
            //    var result = await Task.Factory.StartNew(() => processor.ProcessAsync(WeatherConstants.FullFileName));

            //    Console.WriteLine($"The result is: {result}.");
            //}
            //catch (Exception exception)
            //{
            //    Console.WriteLine($"The application threw the following exception: {exception.Message}.");
            //}

            Console.WriteLine("I hoped you enjoyed it!");

        }
    }
}
