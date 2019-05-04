using System;
using System.IO.Abstractions;
using System.Threading.Tasks;

using DataMungingCore.Interfaces;

namespace WeatherComponent.Processors
{
    public class WeatherReader : IReader
    {
        /// <summary>
        /// The file system that works with the File class.
        /// </summary>
        private readonly IFileSystem _fileSystem;

        public WeatherReader(IFileSystem fileSystem)
        {
            _fileSystem = fileSystem;
        }

        public async Task<string[]> ReadAsync(string fileLocation)
        {            
            // Contract checks.
            if (string.IsNullOrWhiteSpace(fileLocation)) throw new ArgumentNullException(nameof(fileLocation), "The file location can not be null.");

            var file = await Task.Factory.StartNew(() => _fileSystem.File.ReadAllLines(fileLocation));

            return file;
        }
    }
}