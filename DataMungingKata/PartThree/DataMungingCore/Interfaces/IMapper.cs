using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataMungingCore.Interfaces
{
    public interface IMapper
    {
        Task<IList<IDataType>> MapAsync(string[] fileData);
    }
}
