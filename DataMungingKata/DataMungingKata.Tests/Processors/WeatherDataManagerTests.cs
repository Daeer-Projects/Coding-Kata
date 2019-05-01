using System;
using System.Collections.Generic;

using DataMungingKata.Interfaces;
using DataMungingKata.Processors;
using DataMungingKata.Types;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace DataMungingKata.Tests.Processors
{
    public class WeatherDataManagerTests
    {
        private readonly IReader _reader;
        private readonly INotify _notify;
        private WeatherDataManager _weatherDataManager;

        public WeatherDataManagerTests()
        {
            _reader = Substitute.For<IReader>();
            _notify = Substitute.For<INotify>();
            _weatherDataManager = new WeatherDataManager(_reader, _notify);
        }

        [Theory]
        [MemberData(nameof(GetMixedConstructorParameters))]
        public void Test_construction_with_mixed_null_parameters_throws_null_exception(IReader reader, INotify notify)
        {
            // Arrange.
            // Act.
            // Assert.
            Assert.Throws<ArgumentNullException>(() => _weatherDataManager = new WeatherDataManager(reader, notify));
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public void Test_get_day_with_invalid_file_location_throws_exception(string fileLocation)
        {
            // Arrange.
            // Act.
            // Assert.
            Assert.Throws<ArgumentNullException>(() => _weatherDataManager.GetDayWithLeastChange(fileLocation));
        }

        [Fact]
        public void Test_get_day_with_valid_input_and_data_returns_expected_day()
        {
            // Arrange.
            const int expected = 6;
            const string input = "fullFile";

            _reader.GetWeatherData(Arg.Any<string>()).Returns(new List<Weather>());
            _notify.GetDayOfLeastTemperatureChange(Arg.Any<List<Weather>>()).Returns(6);

            // Act.
            var actual = _weatherDataManager.GetDayWithLeastChange(input);

            // Assert.
            actual.Should().Be(expected);
        }

        #region Test Data.

        public static IEnumerable<object[]> GetMixedConstructorParameters
        {
            get
            {
                yield return new object[]
                {
                    null,
                    Substitute.For<INotify>()
                };
                yield return new object[]
                {
                    Substitute.For<IReader>(),
                    null
                };
                yield return new object[]
                {
                    null,
                    null
                };
            }
        }

        #endregion Test Data.
    }
}
