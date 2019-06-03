using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataMungingCoreV2.Interfaces
{
    /// <summary>
    /// The interface for the writer.
    /// This processes the mapped data and produces the result defined by the component.
    /// </summary>
    public interface IWriter
    {
        /// <summary>
        /// The main method used by the writer.
        /// </summary>
        /// <param name="data"> The data to be processed. </param>
        /// <returns>
        /// The answer required, as defined by the component.
        /// </returns>
         Task<IReturnType> WriteAsync(IList<IDataType> data);
    }
}
