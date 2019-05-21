using DataMungingCore.Interfaces;
using Easy.MessageHub;
using FootballComponent.Configuration;
using FootballComponent.Processors;

namespace FootballComponent
{
    public class FootballComponentCreator : IComponentCreator
    {
        private IComponent _footballComponent;

        public IComponent CreateComponent(IMessageHub hub, string fileName)
        {
            var file = FootballConfig.GetFileSystem();
            var logger = FootballConfig.GetLoggerConfiguration();
            var reader = new FootballReader(file, logger);
            var mapper = new FootballMapper(logger);
            var notify = new FootballNotifier(logger);
            var processor = new FootballProcessor(reader, mapper, notify, hub, logger);
            _footballComponent = new Types.FootballComponent(reader, mapper, notify, processor, fileName);

            return _footballComponent;
        }
    }
}
