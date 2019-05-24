using System.IO.Abstractions;
using Serilog;

namespace WeatherComponent.Configuration
{
    /// <summary>
    /// The configuration for the weather component.
    /// </summary>
    public class WeatherConfig
    {
        public const int DayColumnStart = 1;
        public const int DayColumnLength = 4;

        public const int MaxTempColumnStart = 6;
        public const int MaxTempColumnLength = 6;

        public const int MinTempColumnStart = 11;
        public const int MinTempColumnLength = 6;

        public static ILogger GetLoggerConfiguration()
        {
            var weatherLog = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.File("weatherLog.txt", rollingInterval: RollingInterval.Day)
                .CreateLogger();

            return weatherLog;
        }

        public static IFileSystem GetFileSystem()
        {
            return new FileSystem();
        }
    }
}
