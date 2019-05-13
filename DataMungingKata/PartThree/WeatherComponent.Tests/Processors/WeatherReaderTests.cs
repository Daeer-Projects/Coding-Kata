using System;
using System.IO.Abstractions;
using System.Threading.Tasks;
using FluentAssertions;
using NSubstitute;
using Serilog;
using WeatherComponent.Processors;
using Xunit;

namespace WeatherComponent.Tests.Processors
{
    public class WeatherReaderTests
    {
        private readonly IFileSystem _fileSystem;
        private readonly ILogger _logger;
        private readonly WeatherReader _weatherReader;

        public WeatherReaderTests()
        {
            _fileSystem = Substitute.For<IFileSystem>();
            _logger = Substitute.For<ILogger>();
            _weatherReader = new WeatherReader(_fileSystem, _logger);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public async Task Test_get_weather_data_with_null_throws_exception(string input)
        {
            // Arrange.
            // Act.
            // Assert.
            await Assert.ThrowsAsync<ArgumentNullException>(() =>
                _weatherReader.ReadAsync(input));
        }

        [Fact]
        public async Task Test_get_weather_data_and_file_not_found_throws_exception()
        {
            // Arrange.
            const string input = @"C:\Weather.dat";
            _fileSystem.File.ReadAllLines(Arg.Any<string>()).Returns(x => throw new ArgumentNullException());

            // Act.
            // Assert.
            await Assert.ThrowsAsync<ArgumentNullException>(() =>
                _weatherReader.ReadAsync(input));
        }

        [Fact]
        public async Task Test_get_weather_data_returns_expected_data()
        {
            // Arrange.
            const string input = @"C:\Weather.dat";
            string[] data = {"header", "data", "footer"};
            _fileSystem.File.ReadAllLines(Arg.Any<string>()).Returns(data);

            // Act.
            var result = await _weatherReader.ReadAsync(input).ConfigureAwait(false);

            // Assert.
            result.Should().BeEquivalentTo(data);
        }
    }
}
