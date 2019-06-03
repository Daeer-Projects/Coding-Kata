using System.Threading.Tasks;

namespace DataMungingCoreV2.Interfaces
{
    /// <summary>
    /// The interface for the processor.
    /// The processor calls the reader, mapper and writer to get the data and produce the answer required by the component.
    /// </summary>
    public interface IProcessor
    {
        /// <summary>
        /// The main method that kicks of the processing for the component.
        /// </summary>
        /// <param name="fileLocation"> The location of the file that the component is going to process. </param>
        /// <returns>
        /// The result of processing the file.
        /// </returns>
        Task ProcessAsync(string fileLocation);
    }
}
