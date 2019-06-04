using DataMungingCoreV2.Interfaces;

namespace DataMungingCoreV2.Types
{
    /// <summary>
    /// An object that wraps the result of processing.
    /// </summary>
    public class ContainingResultType : IReturnType
    {
        public object ProcessResult { get; set; }
    }
}
