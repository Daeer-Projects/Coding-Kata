using System.Threading.Tasks;

namespace DataMungingCore.Interfaces
{
    public interface IProcessor
    {
        void RegisterSubscriptions();
        Task ProcessAsync(string fileLocation);
    }
}
