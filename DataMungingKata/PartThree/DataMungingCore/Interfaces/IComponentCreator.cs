using Serilog;

using System.IO.Abstractions;

namespace DataMungingCore.Interfaces
{
    public interface IComponentCreator
    {
        IComponent CreateComponent(IFileSystem file, ILogger logger);
    }
}
