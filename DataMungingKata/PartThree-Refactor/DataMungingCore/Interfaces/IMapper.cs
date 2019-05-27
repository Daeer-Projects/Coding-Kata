using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataMungingCoreV2.Interfaces
{
    public interface IMapper
    {
        Task<IList<IDataType>> MapAsync(string[] fileData);
    }
}
