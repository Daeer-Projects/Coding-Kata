using System.Threading.Tasks;

namespace DataMungingCore.Interfaces
{
    public interface IProcessor
    {
        Task ProcessAsync(string fileLocation);
    }
}
