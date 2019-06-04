using System;
using System.Threading.Tasks;

using DataMungingCoreV2.Interfaces;

namespace DataMungingCoreV2.Processors
{
    /// <summary>
    /// The static processor helper.  This is used by the component to process the file through the stages to produce an answer.
    /// </summary>
    public static class Processor
    {
        /// <summary>
        /// This method does the work defined by the component.
        /// This uses all of the components defined reader, mapper and writer, calls their main work method.
        /// </summary>
        /// <param name="fileLocation"> The file location specified by the component. </param>
        /// <param name="reader"> The components created version of the IReader interface. </param>
        /// <param name="mapper"> The components created version of the IMapper interface. </param>
        /// <param name="writer"> The components created version of the IWriter interface. </param>
        /// <returns>
        /// The final result of the processing.
        /// </returns>
        public static async Task<IReturnType> ProcessorWork(string fileLocation, IReader reader, IMapper mapper, IWriter writer)
        {
            // Contract requirements.
            if (string.IsNullOrWhiteSpace(fileLocation)) throw new ArgumentNullException(nameof(fileLocation), "The file location can't be null.");
            if (reader == null) throw new ArgumentNullException(nameof(reader), "The file reader can't be null.");
            if (mapper == null) throw new ArgumentNullException(nameof(mapper), "The data mapper can't be null.");
            if (writer == null) throw new ArgumentNullException(nameof(writer), "The writer can't be null.");

            var readData = await reader.ReadAsync(fileLocation).ConfigureAwait(false);
            var mappedData = await mapper.MapAsync(readData).ConfigureAwait(false);
            var result = await writer.WriteAsync(mappedData).ConfigureAwait(false);

            return result;
        }
    }
}
