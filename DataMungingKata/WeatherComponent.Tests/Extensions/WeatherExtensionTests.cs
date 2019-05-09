using System.Collections.Generic;

using FluentAssertions;
using WeatherComponent.Extensions;
using WeatherComponent.Types;
using Xunit;

namespace WeatherComponent.Tests.Extensions
{
    public class WeatherExtensionTests
    {
        [Theory]
        [MemberData(nameof(GetGoodWeather))]
        public void Test_is_valid_with_valid_weather_returns_true(Weather weather)
        {
            // Arrange.
            // Act.
            var result = weather.IsValid();

            // Assert.
            result.IsValid.Should().BeTrue("the weather data is all made up of valid data.");
        }

        [Theory]
        [MemberData(nameof(GetBadWeather))]
        public void Test_is_valid_with_invalid_weather_returns_false(Weather weather)
        {
            // Arrange.
            // Act.
            var result = weather.IsValid();

            // Assert.
            result.IsValid.Should().BeFalse("the weather data is made up of invalid data.");
        }

        #region Test Data.

        public static IEnumerable<object[]> GetGoodWeather
        {
            get
            {
                yield return new object[]
                {
                    new Weather
                    {
                        Day = 1,
                        MinimumTemperature = float.MinValue,
                        MaximumTemperature = float.MaxValue
                    }
                };
                yield return new object[]
                {
                    new Weather
                    {
                        Day = 31,
                        MinimumTemperature = float.MinValue,
                        MaximumTemperature = float.MaxValue
                    }
                };
                yield return new object[]
                {
                    new Weather
                    {
                        Day = 15,
                        MinimumTemperature = -1452.25f,
                        MaximumTemperature = 4589.25f
                    }
                };
                yield return new object[]
                {
                    new Weather
                    {
                        Day = 15,
                        MinimumTemperature = 100.01f,
                        MaximumTemperature = 100.01f
                    }
                };
            }
        }

        public static IEnumerable<object[]> GetBadWeather
        {
            get
            {
                yield return new object[]
                {
                    new Weather
                    {
                        Day = 0,
                        MinimumTemperature = float.MinValue,
                        MaximumTemperature = float.MaxValue
                    }
                };
                yield return new object[]
                {
                    new Weather
                    {
                        Day = -1,
                        MinimumTemperature = float.MinValue,
                        MaximumTemperature = float.MaxValue
                    }
                };
                yield return new object[]
                {
                    new Weather
                    {
                        Day = -1000,
                        MinimumTemperature = float.MinValue,
                        MaximumTemperature = float.MaxValue
                    }
                };
                yield return new object[]
                {
                    new Weather
                    {
                        Day = int.MinValue,
                        MinimumTemperature = float.MinValue,
                        MaximumTemperature = float.MaxValue
                    }
                };
                yield return new object[]
                {
                    new Weather
                    {
                        Day = 32,
                        MinimumTemperature = float.MinValue,
                        MaximumTemperature = float.MaxValue
                    }
                };
                yield return new object[]
                {
                    new Weather
                    {
                        Day = 69,
                        MinimumTemperature = float.MinValue,
                        MaximumTemperature = float.MaxValue
                    }
                };
                yield return new object[]
                {
                    new Weather
                    {
                        Day = int.MaxValue,
                        MinimumTemperature = float.MinValue,
                        MaximumTemperature = float.MaxValue
                    }
                };
                yield return new object[]
                {
                    new Weather
                    {
                        Day = 15,
                        MinimumTemperature = 100.01f,
                        MaximumTemperature = 100f
                    }
                };
                yield return new object[]
                {
                    new Weather
                    {
                        Day = 15,
                        MinimumTemperature = -100.01f,
                        MaximumTemperature = -100.02f
                    }
                };
            }
        }

        #endregion Test Data.
    }
}
