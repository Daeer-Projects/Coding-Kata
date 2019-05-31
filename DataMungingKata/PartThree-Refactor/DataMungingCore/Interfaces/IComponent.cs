namespace DataMungingCoreV2.Interfaces
{
    public interface IComponent
    {
        IReader Reader { get; }
        IMapper Mapper { get; }
        IWriter Writer { get; }
        IProcessor Processor { get; }
        string FileLocation { get; }
    }
}
