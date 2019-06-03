using System.Threading.Tasks;

namespace DataMungingCoreV2.Interfaces
{
    /// <summary>
    /// The interface for the reader.
    /// The reader takes the file location and extracts the rows into an array for the mapper to process.
    /// </summary>
    public interface IReader
    {
        /// <summary>
        /// The main method of the reader.
        /// </summary>
        /// <param name="fileLocation"> The location of the file to be read in. </param>
        /// <returns>
        /// An array of string objects extracted from the file.
        /// </returns>
        Task<string[]> ReadAsync(string fileLocation);
    }
}
