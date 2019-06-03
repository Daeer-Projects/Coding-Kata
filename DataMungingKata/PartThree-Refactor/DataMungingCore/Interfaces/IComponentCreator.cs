using Easy.MessageHub;

namespace DataMungingCoreV2.Interfaces
{
    /// <summary>
    /// The interface for the component creator.
    /// </summary>
    public interface IComponentCreator
    {
        IComponent CreateComponent(IMessageHub hub, string fileName);
    }
}
