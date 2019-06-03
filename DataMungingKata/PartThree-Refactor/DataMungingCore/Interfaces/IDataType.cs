namespace DataMungingCoreV2.Interfaces
{
    /// <summary>
    /// The interface for the data that is passed between the reader, mapper and writers.
    /// </summary>
    public interface IDataType
    {
        /// <summary>
        /// The data object that is defined by the components.
        /// </summary>
        object Data { get; }
    }
}
