using FluentAssertions;
using Serilog.Core;
using WeatherComponent.Configuration;
using Xunit;

namespace WeatherComponent.Tests.Configuration
{
    public class WeatherConfigTests
    {
        [Fact]
        public void Test_get_logger_returns_logger()
        {
            // Arrange.
            var logger = WeatherConfig.GetLoggerConfiguration();

            // Act.
            // Assert.
            logger.Should().NotBeNull("the serilog system will return the valid logger.");
        }

        [Fact]
        public void Test_get_logger_returns_valid_logger_type()
        {
            // Arrange.
            var logger = WeatherConfig.GetLoggerConfiguration();

            // Act.
            // Assert.
            logger.Should().BeOfType<Logger>("serilog returns the ILogger type.");
        }
    }
}
