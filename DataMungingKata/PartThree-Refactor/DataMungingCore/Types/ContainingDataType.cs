using DataMungingCoreV2.Interfaces;

namespace DataMungingCoreV2.Types
{
    /// <summary>
    /// An object that wraps the data needed to be passed between the processors.
    /// </summary>
    public class ContainingDataType : IDataType
    {
        public object Data { get; set; }
    }
}
