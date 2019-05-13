using System.Collections.Generic;
using System.Linq;

using FluentAssertions;
using WeatherComponent.Types;
using WeatherComponent.Validators;
using Xunit;

namespace WeatherComponent.Tests.Validators
{
    public class WeatherValidatorTests
    {
        private readonly WeatherValidator _weatherValidator;

        public WeatherValidatorTests()
        {
            _weatherValidator = new WeatherValidator();
        }

        [Theory]
        [MemberData(nameof(GetGoodWeather))]
        public void Test_validate_with_good_data_returns_true(Weather weather)
        {
            // Arrange.
            // Act.
            var result = _weatherValidator.Validate(weather);

            // Assert.
            result.IsValid.Should().BeTrue("the weather data is all made up of valid data.");
        }

        [Theory]
        [MemberData(nameof(GetBadWeather))]
        public void Test_validate_with_bad_data_returns_false(Weather weather, int errorCount, List<string> errorMessages)
        {
            // Arrange.
            // Act.
            var result = _weatherValidator.Validate(weather);

            // Assert.
            // If one fails, then it all should fail.  We will see the 'because' detail.
            result.IsValid.Should().BeFalse("the weather data is made up of invalid data.");
            result.Errors.Count.Should().Be(errorCount, "the count should be what I defined.");
            result.Errors.Select(m => m.ErrorMessage).Should().BeEquivalentTo(errorMessages, "the error messages need to match.");
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
                    },
                    1,
                    new List<string>
                        {"Days must be within 1 and 31."}
                };
                yield return new object[]
                {
                    new Weather
                    {
                        Day = -1,
                        MinimumTemperature = float.MinValue,
                        MaximumTemperature = float.MaxValue
                    },
                    1,
                    new List<string>
                        {"Days must be within 1 and 31."}
                };
                yield return new object[]
                {
                    new Weather
                    {
                        Day = -1000,
                        MinimumTemperature = float.MinValue,
                        MaximumTemperature = float.MaxValue
                    },
                    1,
                    new List<string>
                        {"Days must be within 1 and 31."}
                };
                yield return new object[]
                {
                    new Weather
                    {
                        Day = int.MinValue,
                        MinimumTemperature = float.MinValue,
                        MaximumTemperature = float.MaxValue
                    },
                    1,
                    new List<string>
                        {"Days must be within 1 and 31."}
                };
                yield return new object[]
                {
                    new Weather
                    {
                        Day = 32,
                        MinimumTemperature = float.MinValue,
                        MaximumTemperature = float.MaxValue
                    },
                    1,
                    new List<string>
                        {"Days must be within 1 and 31."}
                };
                yield return new object[]
                {
                    new Weather
                    {
                        Day = 69,
                        MinimumTemperature = float.MinValue,
                        MaximumTemperature = float.MaxValue
                    },
                    1,
                    new List<string>
                        {"Days must be within 1 and 31."}
                };
                yield return new object[]
                {
                    new Weather
                    {
                        Day = int.MaxValue,
                        MinimumTemperature = float.MinValue,
                        MaximumTemperature = float.MaxValue
                    },
                    1,
                    new List<string>
                        {"Days must be within 1 and 31."}
                };
                yield return new object[]
                {
                    new Weather
                    {
                        Day = 15,
                        MinimumTemperature = 100.01f,
                        MaximumTemperature = 100f
                    },
                    1,
                    new List<string>
                        {"The Minimum Temperature cannot be greater than the Maximum Temperature."}
                };
                yield return new object[]
                {
                    new Weather
                    {
                        Day = 15,
                        MinimumTemperature = -100.01f,
                        MaximumTemperature = -100.02f
                    },
                    1,
                    new List<string>
                        {"The Minimum Temperature cannot be greater than the Maximum Temperature."}
                };
                yield return new object[]
                {
                    new Weather
                    {
                        Day = 0,
                        MinimumTemperature = -100.01f,
                        MaximumTemperature = -100.02f
                    },
                    2,
                    new List<string>
                    {
                        "Days must be within 1 and 31.",
                        "The Minimum Temperature cannot be greater than the Maximum Temperature."
                    }
                };
            }
        }

        #endregion Test Data.
    }
}
