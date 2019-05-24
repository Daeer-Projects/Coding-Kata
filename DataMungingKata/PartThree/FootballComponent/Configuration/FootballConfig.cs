using System.IO.Abstractions;
using Serilog;

namespace FootballComponent.Configuration
{
    /// <summary>
    /// The configuration settings for the football component.
    /// Should be extracted out as a config file, or database entries.
    /// </summary>
    public class FootballConfig
    {
        public const int TeamColumnStart = 7;
        public const int TeamColumnLength = 16;

        public const int ForColumnStart = 43;
        public const int ForColumnLength = 4;

        public const int AgainstColumnStart = 50;
        public const int AgainstColumnLength = 3;

        public static ILogger GetLoggerConfiguration()
        {
            var footballLog = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.File("footballLog.txt", rollingInterval: RollingInterval.Day)
                .CreateLogger();

            return footballLog;
        }

        public static IFileSystem GetFileSystem()
        {
            return new FileSystem();
        }
    }
}
