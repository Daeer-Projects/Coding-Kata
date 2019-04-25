using System;
using System.Collections.Generic;
using System.IO.Abstractions;
using System.Text;
using DataMungingKata.Processors;
using DataMungingKata.Types;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace DataMungingKata.Tests.Processors
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
        public void Test_get_weather_data_with_null_throws_exception(string input)
        {
            // Arrange.
            // Act.
            // Assert.
            Assert.Throws<ArgumentNullException>(() => _fileExtractor.GetWeatherData(input));
        }

        [Fact]
        public void Test_get_weather_data_and_file_not_found_throws_exception()
        {
            // Arrange.
            const string input = @"C:\Weather.dat";
            _fileSystem.File.ReadAllLines(Arg.Any<string>()).Returns(x => { throw new ArgumentNullException(); });

            // Act.
            // Assert.
            Assert.Throws<ArgumentNullException>(() => _fileExtractor.GetWeatherData(input));
        }

        [Fact]
        public void Test_get_weather_data_with_good_data_returns_expected_list()
        {
            // Arrange.
            var expectedList = new List<Weather>
            {
                new Weather
                {
                    Day = 1,
                    MaximumTemperature = 12.6f,
                    MinimumTemperature = 8.1f
                },
                new Weather
                {
                    Day = 2,
                    MaximumTemperature = 15.4f,
                    MinimumTemperature = 9.3f
                }
            };

            _fileSystem.File.ReadAllLines(Arg.Any<string>()).Returns(GetGoodData());

            // Act.
            var actual = _fileExtractor.GetWeatherData("fileName");

            // Assert.
            actual.Should().BeEquivalentTo(expectedList);
        }

        #region Test Data

        private string[] GetGoodData()
        {
            return new string[]
            {
                "  Dy MxT   MnT   AvT   HDDay  AvDP 1HrP TPcpn WxType PDir AvSp Dir MxS SkyC MxR MnR AvSLP",
                "  ",
                "   1  12.6   8.1  74          53.8       0.00 F       280  9.6 270  17  1.6  93 23 1004.5",
                "   2  15.4   9.3  71          46.5       0.00         330  8.7 340  23  3.3  70 28 1004.5"
            };
        }

        private string[] GetBadData()
        {
            return new string[]
            {
                "  Oh no, not this one!",
                "  ",
                "   47834 2 1.22 424345 yep 12312    43",
                ":)"
            };
        }

        #endregion Test Data
    }
}
