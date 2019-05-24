using System;
using System.IO.Abstractions;
using System.Threading.Tasks;

using FluentAssertions;
using FootballComponent.Processors;
using NSubstitute;
using Serilog;
using Xunit;

namespace FootballComponent.Tests.Processors
{
    public class FootballReaderTests
    {
        private readonly IFileSystem _fileSystem;
        private readonly ILogger _logger;
        private readonly FootballReader _footballReader;

        public FootballReaderTests()
        {
            _fileSystem = Substitute.For<IFileSystem>();
            _logger = Substitute.For<ILogger>();
            _footballReader = new FootballReader(_fileSystem, _logger);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public async Task Test_get_football_data_with_null_throws_exception(string input)
        {
            // Arrange.
            // Act.
            // Assert.
            await Assert.ThrowsAsync<ArgumentNullException>(() =>
                _footballReader.ReadAsync(input)).ConfigureAwait(true);
        }

        [Fact]
        public async Task Test_get_football_data_and_file_not_found_throws_exception()
        {
            // Arrange.
            const string input = @"C:\Football.dat";
            _fileSystem.File.ReadAllLines(Arg.Any<string>()).Returns(x => throw new ArgumentNullException());

            // Act.
            // Assert.
            await Assert.ThrowsAsync<ArgumentNullException>(() =>
                _footballReader.ReadAsync(input)).ConfigureAwait(true);
        }

        [Fact]
        public async Task Test_get_football_data_returns_expected_data()
        {
            // Arrange.
            const string input = @"C:\Weather.dat";
            string[] data = { "header", "data", "footer" };
            _fileSystem.File.ReadAllLines(Arg.Any<string>()).Returns(data);

            // Act.
            var result = await _footballReader.ReadAsync(input).ConfigureAwait(true);

            // Assert.
            result.Should().BeEquivalentTo(data);
        }
    }
}
