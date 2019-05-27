using System;
using System.IO.Abstractions;
using System.Threading.Tasks;
using DataMungingCoreV2.Extensions;
using DataMungingCoreV2.Interfaces;
using Serilog;

namespace FootballComponentV2.Processors
{
    /// <summary>
    /// The reader for the football component.
    /// </summary>
    public class FootballReader : IReader
    {
        private readonly IFileSystem _fileSystem;

        private readonly ILogger _logger;

        public FootballReader(IFileSystem fileSystem, ILogger logger)
        {
            _fileSystem = fileSystem;
            _logger = logger;
        }

        /// <summary>
        /// Reads in the rows from the file.
        /// </summary>
        /// <param name="fileLocation"> The full file path to the file we are reading. </param>
        /// <returns> The rows of data in the file. </returns>
        public async Task<string[]> ReadAsync(string fileLocation)
        {
            _logger.Information($"{GetType().Name} (ReadAsync): Starting reading from file: {fileLocation ?? string.Empty}.");

            // Contract checks.
            if (string.IsNullOrWhiteSpace(fileLocation)) throw new ArgumentNullException(nameof(fileLocation), "The file location can not be null.");
            
            var file = await _fileSystem.File.ReadAllLinesAsync(fileLocation).ConfigureAwait(false);

            _logger.Information($"{GetType().Name} (ReadAsync): Reading complete.");
            return file;
        }
    }
}
