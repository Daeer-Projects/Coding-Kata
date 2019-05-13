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

        [Theory]
        [MemberData(nameof(GetWeatherCalculationData))]
        public void Test_calculate_change_with_data_returns_expected(Weather weather, float expected)
        {
            // Arrange.
            // Act.
            var result = weather.CalculateWeatherChange();

            // Assert.
            result.Should().Be(expected,
                "taking the minimum value from the maximum will produce the expected results.");
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

        public static IEnumerable<object[]> GetWeatherCalculationData
        {
            get
            {
                yield return new object[]
                {
                    new Weather
                    {
                        Day = 1,
                        MinimumTemperature = 101.09f,
                        MaximumTemperature = 102.1f
                    },
                    1.01f
                };
                yield return new object[]
                {
                    new Weather
                    {
                        Day = 2,
                        MinimumTemperature = -101.09f,
                        MaximumTemperature = -99.1f
                    },
                    1.99f
                };
                yield return new object[]
                {
                    new Weather
                    {
                        Day = 3,
                        MinimumTemperature = -14.5f,
                        MaximumTemperature = 2.5f
                    },
                    17.0f
                };
            }
        }

        #endregion Test Data.
    }
}
