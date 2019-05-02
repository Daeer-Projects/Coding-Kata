using System.Collections.Generic;
using DataMungingPartTwo.Types;

namespace DataMungingPartTwo.Interfaces
{
    public interface IReader
    {
        IList<Football> GetFootballData(string fileLocation);
    }
}