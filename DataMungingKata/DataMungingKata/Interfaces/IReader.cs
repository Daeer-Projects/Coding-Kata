using System.Collections.Generic;
using DataMungingKata.Types;

namespace DataMungingKata.Interfaces
{
    public interface IReader
    {
        IList<Weather> GetWeatherData(string fileLocation);
    }
}