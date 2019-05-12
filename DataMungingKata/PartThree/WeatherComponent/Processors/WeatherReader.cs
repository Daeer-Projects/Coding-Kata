using System;
using System.IO.Abstractions;
using System.Threading.Tasks;

using DataMungingCore.Interfaces;
using Serilog;

namespace WeatherComponent.Processors
{
    public class WeatherReader : IReader
    {
        /// <summary>
        /// The file system that works with the File class.
        /// </summary>
        private readonly IFileSystem _fileSystem;

        private readonly ILogger _logger;

        public WeatherReader(IFileSystem fileSystem, ILogger logger)
        {
            _fileSystem = fileSystem;
            _logger = logger;
        }

        public async Task<string[]> ReadAsync(string fileLocation)
        {
            _logger.Information($"{GetType().Name} (ReadAsync): Starting reading from file: {fileLocation ?? string.Empty}.");
            // Contract checks.
            if (string.IsNullOrWhiteSpace(fileLocation)) throw new ArgumentNullException(nameof(fileLocation), "The file location can not be null.");

            var file = await Task.Factory.StartNew(() => _fileSystem.File.ReadAllLines(fileLocation));

            _logger.Information($"{GetType().Name} (ReadAsync): Reading complete.");
            return file;
        }
    }
}