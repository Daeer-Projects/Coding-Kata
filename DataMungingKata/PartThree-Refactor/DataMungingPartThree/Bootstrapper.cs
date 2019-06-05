using System;
using DataMungingCoreV2;
using DataMungingCoreV2.Interfaces;
using Easy.MessageHub;
using FootballComponentV2;
using FootballComponentV2.Constants;
using Serilog;
using WeatherComponentV2;
using WeatherComponentV2.Constants;

namespace DataMungingPartThreeV2
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
            //IComponentCreator weatherComponentCreatorTwo = new WeatherComponentCreator();
            //IComponentCreator weatherComponentCreatorThree = new WeatherComponentCreator();

            coreLogger.Information($"{GetType().Name} (ProcessItemsAsync): Creating 'Football' component.");
            IComponentCreator footballComponentCreator = new FootballComponentCreator();


            var componentRegister = new ComponentRegister(hub, coreLogger);
            var registeredCorrectly = componentRegister.RegisterComponent(weatherComponentCreator, WeatherConstants.FullFileName);
            //registeredCorrectly = componentRegister.RegisterComponent(weatherComponentCreatorTwo, WeatherComponent.Constants.WeatherConstants.FullFileNameTwo);
            //registeredCorrectly = componentRegister.RegisterComponent(weatherComponentCreatorThree, WeatherComponent.Constants.WeatherConstants.FullFileNameThree);
            registeredCorrectly = componentRegister.RegisterComponent(footballComponentCreator,
                FootballConstants.FullFileName);

            componentRegister.RegisterSubscriptions();
            
            try
            {
                // We want to set up the event hub to subscribe to a call to process the registered components.
                // Then we want the components to publish a completed event.
                // Business Business, Numbers... (Psst, is this working?) (Yes) YAAAAYY!!
                
                hub.Publish("Start Processing...");
            }
            catch (Exception exception)
            {
                coreLogger.Error($"{GetType().Name} (ProcessItemsAsync): The application threw the following exception: {exception.Message}.");
            }
        }
    }
}
