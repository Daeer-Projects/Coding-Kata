using Easy.MessageHub;

namespace DataMungingCoreV2.Interfaces
{
    public interface IComponentCreator
    {
        IComponent CreateComponent(IMessageHub hub, string fileName);
    }
}
