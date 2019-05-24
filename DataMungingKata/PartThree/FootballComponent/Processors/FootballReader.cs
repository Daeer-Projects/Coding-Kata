using System;
using System.IO.Abstractions;
using System.Threading.Tasks;

using DataMungingCore.Interfaces;
using Serilog;

namespace FootballComponent.Processors
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

            var file = await Task.Factory.StartNew(() => _fileSystem.File.ReadAllLines(fileLocation)).ConfigureAwait(false);

            _logger.Information($"{GetType().Name} (ReadAsync): Reading complete.");
            return file;
        }
    }
}
