using System.Collections.Generic;
using DataMungingKata.Types;

namespace DataMungingKata.Interfaces
{
    public interface INotify
    {
        int GetDayOfLeastTemperatureChange(IList<Weather> weatherData);
    }
}