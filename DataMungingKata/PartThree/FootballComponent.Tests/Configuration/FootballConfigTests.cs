using FluentAssertions;
using FootballComponent.Configuration;
using Serilog.Core;
using Xunit;

namespace FootballComponent.Tests.Configuration
{
    public class FootballConfigTests
    {
        [Fact]
        public void Test_get_logger_returns_logger()
        {
            // Arrange.
            var logger = FootballConfig.GetLoggerConfiguration();

            // Act.
            // Assert.
            logger.Should().NotBeNull("the serilog system will return the valid logger.");
        }

        [Fact]
        public void Test_get_logger_returns_valid_logger_type()
        {
            // Arrange.
            var logger = FootballConfig.GetLoggerConfiguration();

            // Act.
            // Assert.
            logger.Should().BeOfType<Logger>("serilog returns the ILogger type.");
        }
    }
}
