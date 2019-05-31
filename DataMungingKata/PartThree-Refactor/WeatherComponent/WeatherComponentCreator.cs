using DataMungingCoreV2.Interfaces;
using Easy.MessageHub;
using WeatherComponentV2.Configuration;
using WeatherComponentV2.Processors;

namespace WeatherComponentV2
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
            var writer = new WeatherWriter(logger);
            var processor = new WeatherProcessor(reader, mapper, writer, hub, logger);
            _weatherComponent = new Types.WeatherComponent(reader, mapper, writer, processor, fileName);

            return _weatherComponent;
        }
    }
}
