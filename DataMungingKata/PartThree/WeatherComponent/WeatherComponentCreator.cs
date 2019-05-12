using System.IO.Abstractions;

using DataMungingCore.Interfaces;
using Serilog;
using WeatherComponent.Processors;

namespace WeatherComponent
{
    public class WeatherComponentCreator : IComponentCreator
    {
        public IComponent CreateComponent(IFileSystem file, ILogger logger)
        {
            var reader = new WeatherReader(file, logger);
            var mapper = new WeatherMapper(logger);
            var notify = new WeatherNotifier(logger);
            var processor = new WeatherProcessor(reader, mapper, notify, logger);

            return new Types.WeatherComponent(reader, mapper, notify, processor, Constants.WeatherConstants.FullFileName);
        }
    }
}
