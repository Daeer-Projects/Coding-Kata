namespace DataMungingCoreV2.Interfaces
{
    /// <summary>
    /// The interface for the returning object defined by the components.
    /// </summary>
    public interface IReturnType
    {
        /// <summary>
        /// The result of processing the data.
        /// The type of result is defined by the component.
        /// </summary>
        object ProcessResult { get; }
    }
}
