using DataMungingCore.Interfaces;

namespace FootballComponent.Types
{
    public class FootballComponent : IComponent
    {
        public IReader Reader { get; }
        public IMapper Mapper { get; }
        public INotify Notify { get; }
        public IProcessor Processor { get; }
        public string FileLocation { get; }

        public FootballComponent(IReader reader, IMapper mapper, INotify notify, IProcessor processor, string fileLocation)
        {
            Reader = reader;
            Mapper = mapper;
            Notify = notify;
            Processor = processor;
            FileLocation = fileLocation;
        }
    }
}
