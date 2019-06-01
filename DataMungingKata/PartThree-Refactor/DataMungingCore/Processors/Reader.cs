using System;
using System.IO.Abstractions;
using System.Threading.Tasks;

namespace DataMungingCoreV2.Processors
{
    public static class Reader
    {
        public static Task<string[]> ReadWork(IFile fileSystem, string fileLocation)
        {
            if (fileSystem == null) throw new ArgumentNullException(nameof(fileSystem), "The file system must not be null.");
            if (fileLocation == null) throw new ArgumentNullException(nameof(fileLocation), "The file location for processing must not be null.");
            if (string.IsNullOrWhiteSpace(fileLocation)) throw new ArgumentException(nameof(fileLocation), "The file location must contain a valid path and file.");

            return Task.Factory.StartNew(() => fileSystem.ReadAllLines(fileLocation));
        }
    }
}
