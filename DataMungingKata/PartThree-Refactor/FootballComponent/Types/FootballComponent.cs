using DataMungingCoreV2.Interfaces;

namespace FootballComponentV2.Types
{
    public class FootballComponent : IComponent
    {
        public IReader Reader { get; }
        public IMapper Mapper { get; }
        public IWriter Writer { get; }
        public IProcessor Processor { get; }
        public string FileLocation { get; }

        public FootballComponent(IReader reader, IMapper mapper, IWriter writer, IProcessor processor, string fileLocation)
        {
            Reader = reader;
            Mapper = mapper;
            Writer = writer;
            Processor = processor;
            FileLocation = fileLocation;
        }
    }
}
