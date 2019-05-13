using System;
using System.Collections.Generic;
using System.IO.Abstractions;
using System.Threading.Tasks;

using DataMungingCore;
using DataMungingCore.Interfaces;
using Easy.MessageHub;
using Serilog;
using WeatherComponent;

namespace DataMungingPartThree
{
    public class Bootstrapper
    {
        public void ProcessItemsAsync()
        {
            // Need to set up the logs.
            // Need to set up the event system.
            // Need to create everything.
            // Need to register the component(s).
            // Need to raise the start event.
            // Need to subscribe to the completed events for each component.
            
            var coreLogger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Console()
                .WriteTo.File("coreLog.txt", rollingInterval: RollingInterval.Day)
                .CreateLogger();

             var hub = MessageHub.Instance;

            coreLogger.Information($"{GetType().Name} (ProcessItemsAsync): Logs created.");
            coreLogger.Information($"{GetType().Name} (ProcessItemsAsync): Creating components...");

            coreLogger.Information($"{GetType().Name} (ProcessItemsAsync): Creating 'Weather' component.");
            IComponentCreator weatherComponentCreator = new WeatherComponentCreator();


            var componentRegister = new ComponentRegister(hub, coreLogger);
            var registeredCorrectly = componentRegister.RegisterComponent(weatherComponentCreator);

            // Does this work?
            hub.Subscribe<IReturnType>(r => coreLogger.Information($"The result is: {r.ProcessResult}."));

            // Apparently so.
            
            try
            {
                // We don't want to call this anymore, what we want to do is set up the event hub to subscribe
                // to a call to process the registered components.
                // Then we want the components to publish a completed event.
                // Business Business, Numbers... (Psst, is this working?) (Yes) YAAAAYY!!

                //await componentRegister.ProcessComponents().ConfigureAwait(false);

                hub.Publish("Start Processing...");
            }
            catch (Exception exception)
            {
                coreLogger.Error($"{GetType().Name} (ProcessItemsAsync): The application threw the following exception: {exception.Message}.");
            }
        }
    }
}
