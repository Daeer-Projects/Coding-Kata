namespace DataMungingCore.Interfaces
{
    public interface IComponent
    {
        IReader Reader { get; }
        IMapper Mapper { get; }
        INotify Notify { get; }
        IProcessor Processor { get; }
        string FileLocation { get; }
    }
}
