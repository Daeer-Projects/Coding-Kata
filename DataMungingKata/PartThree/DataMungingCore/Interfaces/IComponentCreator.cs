using Easy.MessageHub;

namespace DataMungingCore.Interfaces
{
    public interface IComponentCreator
    {
        IComponent CreateComponent(IMessageHub hub, string fileName);
    }
}
