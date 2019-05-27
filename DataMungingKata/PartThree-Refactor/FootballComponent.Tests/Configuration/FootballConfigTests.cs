using FluentAssertions;
using FootballComponentV2.Configuration;
using Serilog.Core;
using Xunit;

namespace FootballComponentV2.Tests.Configuration
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
