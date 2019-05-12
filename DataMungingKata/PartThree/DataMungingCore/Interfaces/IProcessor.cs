using System.Threading.Tasks;

namespace DataMungingCore.Interfaces
{
    public interface IProcessor
    {
        Task<IReturnType> ProcessAsync(string fileLocation);
        //IReturnType ProcessAsync(string fileLocation);
    }
}
