using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Abstractions;
using System.Linq;
using DataMungingKata.Constants;
using DataMungingKata.Interfaces;
using DataMungingKata.Types;

namespace DataMungingKata.Processors
{

    /// <summary>
    /// A system to extract data from a file.  Specifically the "<see cref="Weather"/>" data.
    /// </summary>
    public class FileExtractor : IReader
    {
        /// <summary>
        /// The file system that works with the File class.
        /// </summary>
        private readonly IFileSystem _fileSystem;

        /// <summary>
        /// Initialises a new instance of the FileExtractor class.
        /// </summary>
        /// <param name="fileSystem"> The file system we are using to read a file. </param>
        public FileExtractor(IFileSystem fileSystem)
        {
            _fileSystem = fileSystem;
        }


        /// <summary>
        /// Gets the weather data from a file specified.
        /// </summary>
        /// <param name="fileLocation"> The location of the file we are going to extract the data from. </param>
        /// <exception cref="ArgumentNullException">
        /// If the file locations is <see langword="null"/> or empty.
        /// </exception>
        /// <exception cref="DirectoryNotFoundException">
        /// The specified path is invalid (for example, it is on an unmapped
        /// drive).
        /// </exception>
        /// <exception cref="System.IO.IOException">
        /// An I/O error occurred while opening the file.
        /// </exception>
        /// <exception cref="UnauthorizedAccessException">
        /// <paramref name="path" /> specified a file that is read-only.-or-
        /// This operation is not supported on the current platform.-or-
        /// <paramref name="path" /> specified a directory.-or- The caller does
        /// not have the required permission.
        /// </exception>
        /// <exception cref="FileNotFoundException">
        /// The file specified in <paramref name="path" /> was not found.
        /// </exception>
        /// <exception cref="System.Security.SecurityException">
        /// The caller does not have the required permission.
        /// </exception>
        /// <exception cref="PathTooLongException">
        /// The specified path, file name, or both exceed the system-defined
        /// maximum length. For example, on Windows-based platforms, paths must
        /// be less than 248 characters, and file names must be less than 260
        /// characters.
        /// </exception>
        /// <exception cref="NotSupportedException">
        /// <paramref name="path" /> is in an invalid format.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// <paramref name="path" /> is a zero-length string, contains only
        /// white space, or contains one or more invalid characters as defined
        /// by <see cref="Path.InvalidPathChars" /> .
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="startIndex" /> plus <paramref name="length" />
        /// indicates a position not within this instance. -or-
        /// <paramref name="startIndex" /> or <paramref name="length" /> is less
        /// than zero.
        /// </exception> 
        /// <exception cref="InvalidOperationException">The source sequence is empty.</exception>
        /// <exception cref="InvalidDataException">Invalid File Data.  No rows found.</exception>
        /// <returns>
        /// A list of weather items.
        /// </returns>
        public IList<Weather> GetWeatherData(string fileLocation)
        {
            // Contract checks.
            if (string.IsNullOrWhiteSpace(fileLocation)) throw new ArgumentNullException(nameof(fileLocation), "The file location can not be null.");

            var file = _fileSystem.File.ReadAllLines(fileLocation);
            if (!file.Any() || !file.First().Contains(AppConstants.WeatherHeader)) throw new InvalidDataException("Invalid File Data.  No rows found.");

            var results = new List<Weather>();

            foreach (var item in file)
            {
                // Need to use the config to extract out the items...
                if (!item.Equals(AppConstants.WeatherHeader) && !string.IsNullOrWhiteSpace(item) && !item.Contains("mo"))
                {
                    // So, not the header and not the empty line.
                    var day = item.Substring(WeatherConfig.DayColumnStart, WeatherConfig.DayColumnLength);
                    var maxTemp = item.Substring(WeatherConfig.MaxTempColumnStart, WeatherConfig.MaxTempColumnLength);
                    var minTemp = item.Substring(WeatherConfig.MinTempColumnStart, WeatherConfig.MinTempColumnLength);

                    if (int.TryParse(day, out var dayAsInt) &&
                        float.TryParse(maxTemp, out var maxTempAsFloat) &&
                        float.TryParse(minTemp, out var minTempAsFloat))
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
