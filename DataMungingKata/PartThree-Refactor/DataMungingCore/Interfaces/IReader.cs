using System.Threading.Tasks;

namespace DataMungingCoreV2.Interfaces
{
    public interface IReader
    {
        Task<string[]> ReadAsync(string fileLocation);
    }
}
