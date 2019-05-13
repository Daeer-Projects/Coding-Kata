using System.Threading.Tasks;

namespace DataMungingCore.Interfaces
{
    public interface IReader
    {
        Task<string[]> ReadAsync(string fileLocation);
    }
}
