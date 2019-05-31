using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataMungingCoreV2.Interfaces
{
    public interface IWriter
    {
         Task<IReturnType> WriteAsync(IList<IDataType> data);
    }
}
