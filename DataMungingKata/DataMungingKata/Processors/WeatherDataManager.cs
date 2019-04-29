using DataMungingKata.Interfaces;

namespace DataMungingKata.Processors
{
    public class WeatherDataManager
    {
        private readonly IReader _fileReader;
        private readonly INotify _weatherData;

        public WeatherDataManager(IReader reader, INotify notify)
        {
            _fileReader = reader;
            _weatherData = notify;
        }


        public int GetDayWithLeastChange(string fileLocation)
        {
            // ToDo: Add unit tests and contract checks.
            var fileData = _fileReader.GetWeatherData(fileLocation);
            int result = _weatherData.GetDayOfLeastTemperatureChange(fileData);

            return result;
        }
    }
}
