using System;
using System.IO.Abstractions;
using System.Threading.Tasks;

using DataMungingCore.Interfaces;
using DataMungingCore.Types;
using Serilog;
using WeatherComponent.Constants;
using WeatherComponent.Processors;

namespace DataMungingPartThree
{
    public class Bootstrapper
    {
        public async Task<IReturnType> ProcessItemsAsync()
        {
            // Need to set up the logs.
            // Need to set up the event system.
            // Need to create everything.
            // Need to register the component(s).
            // Need to raise the start event.
            // Need to subscribe to the completed events for each component.

            var weatherLog = new LoggerConfiguration()
                .WriteTo.Console()
                .WriteTo.File("weatherLog.txt", rollingInterval: RollingInterval.Day)
                .CreateLogger();

            var coreLogger = new LoggerConfiguration()
                .WriteTo.Console()
                .WriteTo.File("coreLog.txt", rollingInterval: RollingInterval.Day)
                .CreateLogger();

            coreLogger.Information($"{GetType().Name} (ProcessItemsAsync): Logs created.");
            coreLogger.Information($"{GetType().Name} (ProcessItemsAsync): Creating components...");

            coreLogger.Information($"{GetType().Name} (ProcessItemsAsync): Creating 'Weather' component.");
            var reader = new WeatherReader(new FileSystem(), weatherLog);
            var mapper = new WeatherMapper(weatherLog);
            var notifier = new WeatherNotifier(weatherLog);
            var processor = new WeatherProcessor(reader, mapper, notifier, weatherLog);
            IReturnType result = new ContainingResultType {ProcessResult = 0};

            //Console.WriteLine($"Processing the file '{WeatherConstants.FullFileName}'.");

            coreLogger.Information($"{GetType().Name} (ProcessItemsAsync): Processing the file: '{WeatherConstants.FullFileName}'");
            try
            {
                 result = await processor.ProcessAsync(WeatherConstants.FullFileName).ConfigureAwait(false);
                 coreLogger.Information($"{GetType().Name} (ProcessItemsAsync): Weather complete. Result: {result.ProcessResult}.");
            }
            catch (Exception exception)
            {
                //Console.WriteLine($"The application threw the following exception: {exception.Message}.");
                coreLogger.Error($"{GetType().Name} (ProcessItemsAsync): The application threw the following exception: {exception.Message}.");
            }

            return result;
        }
    }
}
