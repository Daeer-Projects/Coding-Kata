using System;
using System.Collections.Generic;

using DataMungingKata.Processors;
using DataMungingKata.Types;
using FluentAssertions;
using Xunit;

namespace DataMungingKata.Tests.Processors
{
    public class WeatherDataTests
    {
        private readonly WeatherData _weatherData;

        public WeatherDataTests()
        {
            _weatherData = new WeatherData();
        }

        [Fact]
        public void Test_get_day_with_empty_list_throws_exception()
        {
            // Arrange.
            var data = new List<Weather>();

            // Act.
            // Assert.
            Assert.Throws<ArgumentException>(() => _weatherData.GetDayOfLeastTemperatureChange(data));
        }

        [Fact]
        public void Test_get_day_with_null_list_throws_null_exception()
        {
            // Arrange.
            // Act.
            // Assert.
            Assert.Throws<ArgumentNullException>(() => _weatherData.GetDayOfLeastTemperatureChange(null));
        }

        [Theory]
        [MemberData(nameof(GetValidWeatherData))]
        public void Test_get_day_with_valid_list_returns_expected(int expectedDay, IList<Weather> data)
        {
            // Arrange.
            // Act.
            var actualDay = _weatherData.GetDayOfLeastTemperatureChange(data);

            // Assert.
            actualDay.Should().Be(expectedDay);
        }

        #region Test Data.

        public static IEnumerable<object[]> GetValidWeatherData
        {
            get
            {
                yield return new object[]
                {
                    1,
                    new List<Weather>
                    {
                        new Weather {Day = 1, MaximumTemperature = 21.4f, MinimumTemperature = 20.4f},
                        new Weather {Day = 2, MaximumTemperature = 25.4f, MinimumTemperature = 20.1f}
                    }
                };
                yield return new object[]
                {
                    3,
                    new List<Weather>
                    {
                        new Weather {Day = 1, MaximumTemperature = 21.4f, MinimumTemperature = 20.4f},
                        new Weather {Day = 2, MaximumTemperature = 25.4f, MinimumTemperature = 20.1f},
                        new Weather {Day = 3, MaximumTemperature = 21.1f, MinimumTemperature = 20.4f}
                    }
                };
                yield return new object[]
                {
                    2,
                    new List<Weather>
                    {
                        new Weather {Day = 1, MaximumTemperature = -20.4f, MinimumTemperature = -121.5f},
                        new Weather {Day = 2, MaximumTemperature = -117.3f, MinimumTemperature = -119.7f},
                        new Weather {Day = 3, MaximumTemperature = -3.4f, MinimumTemperature = -21.1f},
                        new Weather {Day = 3, MaximumTemperature = 2.1f, MinimumTemperature = -2.1f}
                    }
                };
            }
        }

        #endregion Test Data.
    }
}