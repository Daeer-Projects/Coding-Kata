using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataMungingCoreV2.Interfaces
{
    public interface INotify
    {
         Task<IReturnType> NotifyAsync(IList<IDataType> data);
    }
}
