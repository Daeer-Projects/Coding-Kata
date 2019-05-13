using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataMungingCore.Interfaces
{
    public interface INotify
    {
         Task<IReturnType> NotifyAsync(IList<IDataType> data);
    }
}
