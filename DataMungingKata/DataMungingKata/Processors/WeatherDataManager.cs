using System;

using DataMungingKata.Interfaces;

namespace DataMungingKata.Processors
{
    /// <summary>
    /// A system to set up and process the data in the file specified.
    /// Uses a system to extract the data from a file, and then process that data
    /// to find the answer required.
    /// </summary>
    public class WeatherDataManager
    {
        /// <summary>
        /// The file extractor.
        /// </summary>
        private readonly IReader _fileReader;

        /// <summary>
        /// The data processor.
        /// </summary>
        private readonly INotify _weatherData;

        /// <summary>
        /// Initialises a new instance of the WeatherDataManager.
        /// </summary>
        /// <param name="reader"> The file extractor. </param>
        /// <param name="notify"> The data processor. </param>
        public WeatherDataManager(IReader reader, INotify notify)
        {
            // Contract requirements.
            if (reader is null) throw new ArgumentNullException(nameof(reader), "The file extractor can not be null.");
            if (notify is null) throw new ArgumentNullException(nameof(notify), "The data processor can not be null.");

            _fileReader = reader;
            _weatherData = notify;
        }

        /// <summary>
        /// Gets the day with the least change from the file / data provided.
        /// </summary>
        /// <param name="fileLocation"> The location of the file that contains our data. </param>
        /// <returns>
        /// The day entry in the file for the day that has the least temperature change.
        /// </returns>
        public int GetDayWithLeastChange(string fileLocation)
        {
            // Contract requirements.
            if (string.IsNullOrWhiteSpace(fileLocation)) throw new ArgumentNullException(nameof(fileLocation), "The file location can not be null.");

            var fileData = _fileReader.GetWeatherData(fileLocation);
            int result = _weatherData.GetDayOfLeastTemperatureChange(fileData);

            return result;
        }
    }
}
