using DataMungingCoreV2.Interfaces;
using Easy.MessageHub;
using FootballComponentV2.Configuration;
using FootballComponentV2.Processors;

namespace FootballComponentV2
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
            var writer = new FootballWriter(logger);
            var processor = new FootballProcessor(reader, mapper, writer, hub, logger);
            _footballComponent = new Types.FootballComponent(reader, mapper, writer, processor, fileName);

            return _footballComponent;
        }
    }
}
