using System.Threading.Tasks;

namespace DataMungingCoreV2.Interfaces
{
    public interface IProcessor
    {
        Task ProcessAsync(string fileLocation);
    }
}
