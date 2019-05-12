using DataMungingCore.Interfaces;

namespace DataMungingCore.Types
{
    public class ContainingResultType : IReturnType
    {
        public object ProcessResult { get; set; }
    }
}
