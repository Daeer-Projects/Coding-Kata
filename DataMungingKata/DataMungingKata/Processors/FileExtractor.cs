using System;
using System.Collections.Generic;
using System.IO.Abstractions;

using DataMungingKata.Constants;
using DataMungingKata.Types;

namespace DataMungingKata.Processors
{
    public class FileExtractor
    {
        private readonly IFileSystem _fileSystem;

        public FileExtractor(IFileSystem fileSystem)
        {
            _fileSystem = fileSystem;
        }

        public IList<Weather> GetWeatherData(string fileLocation)
        {
            // Contract checks.
            if (string.IsNullOrWhiteSpace(fileLocation)) throw new ArgumentNullException();

            var file = _fileSystem.File.ReadAllLines(fileLocation);
            var results = new List<Weather>();

            foreach(var item in file)
            {
                // Need to use the config to extract out the items...
                if (!item.Equals(AppConstants.WeatherHeader) && !string.IsNullOrWhiteSpace(item))
                {
                    // So, not the header and not the empty line.
                    var day = item.Substring(WeatherConfig.DayColumnStart, WeatherConfig.DayColumnLength);
                    var maxTemp = item.Substring(WeatherConfig.MaxTempColumnStart, WeatherConfig.MaxTempColumnLength);
                    var minTemp = item.Substring(WeatherConfig.MinTempColumnStart, WeatherConfig.MinTempColumnLength);

                    var dayAsInt = 0;
                    var maxTempAsFloat = 0.0f;
                    var minTempAsFloat = 0.0f;

                    if (int.TryParse(day, out dayAsInt) && float.TryParse(maxTemp, out maxTempAsFloat) && float.TryParse(minTemp, out minTempAsFloat))
                    {
                        // So all of them parsed correctly.
                        var currentWeather = new Weather
                        {
                            Day = dayAsInt,
                            MaximumTemperature = maxTempAsFloat,
                            MinimumTemperature = minTempAsFloat
                        };

                        results.Add(currentWeather);
                    }
                }
            }

            return results;
        }
    }
}
