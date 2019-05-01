using System;
using System.Collections.Generic;
using System.IO.Abstractions;

using DataMungingPartTwo.Processors;
using DataMungingPartTwo.Types;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace DataMungingPartTwo.Tests.Processors
{
    public class FileExtractorTests
    {
        private readonly IFileSystem _fileSystem;
        private readonly FileExtractor _fileExtractor;

        public FileExtractorTests()
        {
            _fileSystem = Substitute.For<IFileSystem>();
            _fileExtractor = new FileExtractor(_fileSystem);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void Test_get_football_data_with_null_throws_exception(string input)
        {
            // Arrange.
            // Act.
            // Assert.
            Assert.Throws<ArgumentNullException>(() => _fileExtractor.GetFootballData(input));
        }

        [Fact]
        public void Test_get_football_data_and_file_not_found_throws_exception()
        {
            // Arrange.
            const string input = @"C:\Football.dat";
            _fileSystem.File.ReadAllLines(Arg.Any<string>()).Returns(x => throw new ArgumentNullException());

            // Act.
            // Assert.
            Assert.Throws<ArgumentNullException>(() => _fileExtractor.GetFootballData(input));
        }

        [Fact]
        public void Test_get_football_data_with_good_data_returns_expected_list()
        {
            // Arrange.
            var expectedList = new List<Football>
            {
                new Football
                {
                    TeamName = "Arsenal",
                    ForPoints = 79,
                    AgainstPoints = 36
                },
                new Football
                {
                    TeamName = "Liverpool",
                    ForPoints = 67,
                    AgainstPoints = 30
                },
                new Football
                {
                    TeamName = "Manchester_U",
                    ForPoints = 87,
                    AgainstPoints = 45
                },
                new Football
                {
                    TeamName = "Newcastle",
                    ForPoints = 74,
                    AgainstPoints = 52
                }
            };

            _fileSystem.File.ReadAllLines(Arg.Any<string>()).Returns(GetGoodData());

            // Act.
            var actual = _fileExtractor.GetFootballData("fileName");

            // Assert.
            actual.Should().BeEquivalentTo(expectedList);
        }

        #region Test Data.

        private string[] GetGoodData()
        {
            return new string[]
            {
                "       Team            P     W    L   D    F      A     Pts",
                "    1. Arsenal         38    26   9   3    79  -  36    87",
                "    2. Liverpool       38    24   8   6    67  -  30    80",
                "    3. Manchester_U    38    24   5   9    87  -  45    77",
                "   -------------------------------------------------------",
                "    4. Newcastle       38    21   8   9    74  -  52    71"
            };
        }

        #endregion Test Data.
    }
}
