using DataMungingCoreV2.Interfaces;

namespace WeatherComponentV2.Types
{
    public class WeatherComponent : IComponent
    {
        public IReader Reader { get; }
        public IMapper Mapper { get; }
        public IWriter Writer { get; }
        public IProcessor Processor { get; }
        public string FileLocation { get; }

        public WeatherComponent(IReader reader, IMapper mapper, IWriter writer, IProcessor processor, string fileLocation)
        {
            Reader = reader;
            Mapper = mapper;
            Writer = writer;
            Processor = processor;
            FileLocation = fileLocation;
        }
    }
}
