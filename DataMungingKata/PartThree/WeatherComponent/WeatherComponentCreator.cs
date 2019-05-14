using DataMungingCore.Interfaces;
using Easy.MessageHub;
using WeatherComponent.Configuration;
using WeatherComponent.Processors;

namespace WeatherComponent
{
    public class WeatherComponentCreator : IComponentCreator
    {
        private IComponent _weatherComponent;

        public IComponent CreateComponent(IMessageHub hub, string fileName)
        {
            var file = WeatherConfig.GetFileSystem();
            var logger = WeatherConfig.GetLoggerConfiguration();
            var reader = new WeatherReader(file, logger);
            var mapper = new WeatherMapper(logger);
            var notify = new WeatherNotifier(logger);
            var processor = new WeatherProcessor(reader, mapper, notify, hub, logger);
            _weatherComponent = new Types.WeatherComponent(reader, mapper, notify, processor, fileName);

            return _weatherComponent;
        }
    }
}
