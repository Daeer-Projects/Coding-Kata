using System;
using System.IO.Abstractions;
using System.Threading.Tasks;

using NSubstitute;
using WeatherComponent.Processors;
using Xunit;

namespace WeatherComponent.Tests.Processors
{
    public class WeatherReaderTests
    {
        private readonly IFileSystem _fileSystem;
        private readonly WeatherReader _weatherReader;

        public WeatherReaderTests()
        {
            _fileSystem = Substitute.For<IFileSystem>();
            _weatherReader = new WeatherReader(_fileSystem);
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
    }
}
