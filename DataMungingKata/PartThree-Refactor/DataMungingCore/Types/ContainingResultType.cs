using DataMungingCoreV2.Interfaces;

namespace DataMungingCoreV2.Types
{
    public class ContainingResultType : IReturnType
    {
        public object ProcessResult { get; set; }
    }
}
