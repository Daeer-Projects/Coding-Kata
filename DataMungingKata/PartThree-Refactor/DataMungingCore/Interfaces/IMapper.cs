using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataMungingCoreV2.Interfaces
{
    /// <summary>
    /// The mapper interface.
    /// The mapper takes the data that has been read in, and maps it to a class defined by the component.
    /// </summary>
    public interface IMapper
    {
        /// <summary>
        /// The main method of the mapper.
        /// </summary>
        /// <param name="fileData"> The data from the file that has been read in. </param>
        /// <returns>
        /// The mapped data into classes that the component has defined.
        /// </returns>
        Task<IList<IDataType>> MapAsync(string[] fileData);
    }
}
