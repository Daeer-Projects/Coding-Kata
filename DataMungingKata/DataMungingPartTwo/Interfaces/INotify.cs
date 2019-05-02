using System.Collections.Generic;
using DataMungingPartTwo.Types;

namespace DataMungingPartTwo.Interfaces
{
    public interface INotify
    {
        string GetTeamWithSmallestPointRange(IList<Football> footballData);
    }
}