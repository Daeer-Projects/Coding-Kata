namespace DataMungingCoreV2.Interfaces
{
    /// <summary>
    /// The interface for the component type.
    /// </summary>
    public interface IComponent
    {
        IReader Reader { get; }
        IMapper Mapper { get; }
        IWriter Writer { get; }
        IProcessor Processor { get; }
        string FileLocation { get; }
    }
}
