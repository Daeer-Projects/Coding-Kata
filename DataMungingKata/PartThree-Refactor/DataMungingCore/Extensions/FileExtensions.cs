using System.IO.Abstractions;
using System.Threading.Tasks;

namespace DataMungingCoreV2.Extensions
{
    public static class FileExtensions
    {
        public static Task<string[]> ReadAllLinesAsync(this IFile fileSystem, string fileLocation)
        {
            return Task.Factory.StartNew(() => fileSystem.ReadAllLines(fileLocation));
        }
    }
}
