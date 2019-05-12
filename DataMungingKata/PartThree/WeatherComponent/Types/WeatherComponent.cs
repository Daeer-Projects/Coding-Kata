using DataMungingCore.Interfaces;

namespace WeatherComponent.Types
{
    public class WeatherComponent : IComponent
    {
        public IReader Reader { get; }
        public IMapper Mapper { get; }
        public INotify Notify { get; }
        public IProcessor Processor { get; }
        public string FileLocation { get; }

        public WeatherComponent(IReader reader, IMapper mapper, INotify notify, IProcessor processor, string fileLocation)
        {
            Reader = reader;
            Mapper = mapper;
            Notify = notify;
            Processor = processor;
            FileLocation = fileLocation;
        }
    }
}
