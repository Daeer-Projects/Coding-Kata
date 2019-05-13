using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using DataMungingCore.Interfaces;
using DataMungingCore.Types;
using FluentAssertions;
using NSubstitute;
using Serilog;
using WeatherComponent.Processors;
using WeatherComponent.Types;
using Xunit;

namespace WeatherComponent.Tests.Processors
{
    public class WeatherNotifierTests
    {
        private readonly ILogger _logger;
        private readonly WeatherNotifier _weatherNotifier;

        public WeatherNotifierTests()
        {
            _logger = Substitute.For<ILogger>();
            _weatherNotifier = new WeatherNotifier(_logger);
        }

        [Fact]
        public async Task Test_get_day_with_empty_list_throws_exception()
        {
            // Arrange.
            IList<IDataType> data = new List<IDataType>();

            // Act.
            // Assert.
            await Assert.ThrowsAsync<ArgumentException>(() => _weatherNotifier.NotifyAsync(data));
        }

        [Theory]
        [MemberData(nameof(GetInValidWeatherData))]
        public async Task Test_get_day_with_min_greater_than_max_temp_throws_exception(IList<IDataType> data)
        {
            // Arrange.
            // Act.
            // Assert.
            await Assert.ThrowsAsync<ArgumentException>(() => _weatherNotifier.NotifyAsync(data));
        }
        
        [Fact]
        public async Task Test_get_day_with_null_list_throws_null_exception()
        {
            // Arrange.
            // Act.
            // Assert.
            await Assert.ThrowsAsync<ArgumentNullException>(() => _weatherNotifier.NotifyAsync(null));
        }

        [Theory]
        [MemberData(nameof(GetValidWeatherData))]
        public async Task Test_get_day_with_valid_list_returns_expected(int expectedDay, IList<IDataType> data)
        {
            // Arrange.
            // Act.
            var actualDay = await _weatherNotifier.NotifyAsync(data).ConfigureAwait(false);

            // Assert.
            actualDay.ProcessResult.Should().Be(expectedDay);
        }

        #region Test Data.

        public static IEnumerable<object[]> GetValidWeatherData
        {
            get
            {
                yield return new object[]
                {
                    1,
                    new List<IDataType>
                    {
                        new ContainingDataType
                            {Data = new Weather {Day = 1, MaximumTemperature = 21.4f, MinimumTemperature = 20.4f}},
                        new ContainingDataType
                            {Data = new Weather {Day = 2, MaximumTemperature = 25.4f, MinimumTemperature = 20.1f}}
                    }
                };
                yield return new object[]
                {
                    3,
                    new List<IDataType>
                    {
                        new ContainingDataType
                            {Data = new Weather {Day = 1, MaximumTemperature = 21.4f, MinimumTemperature = 20.4f}},
                        new ContainingDataType
                            {Data = new Weather {Day = 2, MaximumTemperature = 25.4f, MinimumTemperature = 20.1f}},
                        new ContainingDataType
                            {Data = new Weather {Day = 3, MaximumTemperature = 21.1f, MinimumTemperature = 20.4f}}
                    }
                };
                yield return new object[]
                {
                    2,
                    new List<IDataType>
                    {
                        new ContainingDataType
                            {Data = new Weather {Day = 1, MaximumTemperature = -20.4f, MinimumTemperature = -121.5f}},
                        new ContainingDataType
                            {Data = new Weather {Day = 2, MaximumTemperature = -117.3f, MinimumTemperature = -119.7f}},
                        new ContainingDataType
                            {Data = new Weather {Day = 3, MaximumTemperature = -3.4f, MinimumTemperature = -21.1f}},
                        new ContainingDataType
                            {Data = new Weather {Day = 3, MaximumTemperature = 2.1f, MinimumTemperature = -2.1f}}
                    }
                };
            }
        }

        public static IEnumerable<object[]> GetInValidWeatherData
        {
            get
            {
                yield return new object[]
                {
                    new List<IDataType>
                    {
                        new ContainingDataType
                            {Data = new Weather {Day = 1, MaximumTemperature = 21.4f, MinimumTemperature = 30f}}
                    }
                };
            }
        }

        #endregion Test Data.
    }
}
