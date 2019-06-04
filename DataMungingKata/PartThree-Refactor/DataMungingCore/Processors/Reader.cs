using System;
using System.IO.Abstractions;
using System.Threading.Tasks;

namespace DataMungingCoreV2.Processors
{
    /// <summary>
    /// The static reader helper.  This is used by the component to read in the data from a file.
    /// It will pass on the data to the mapper.
    /// </summary>
    public static class Reader
    {
        /// <summary>
        /// This method does the work required by the components reader.
        /// The component will call this with the file location specific for that component.
        /// This simply wraps the synchronous call into a task.
        /// </summary>
        /// <param name="fileSystem"> The file system set up by the component. </param>
        /// <param name="fileLocation"> The location of the file defined by the component. </param>
        /// <returns>
        /// The data from the file as a string array.
        /// </returns>
        public static Task<string[]> ReadWork(IFile fileSystem, string fileLocation)
        {
            if (fileSystem == null) throw new ArgumentNullException(nameof(fileSystem), "The file system must not be null.");
            if (fileLocation == null) throw new ArgumentNullException(nameof(fileLocation), "The file location for processing must not be null.");
            if (string.IsNullOrWhiteSpace(fileLocation)) throw new ArgumentException(nameof(fileLocation), "The file location must contain a valid path and file.");

            return Task.Factory.StartNew(() => fileSystem.ReadAllLines(fileLocation));
        }
    }
}
