using System;
using System.Threading.Tasks;

using DataMungingCoreV2.Interfaces;

namespace DataMungingCoreV2.Processors
{
    public static class Processor
    {
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
