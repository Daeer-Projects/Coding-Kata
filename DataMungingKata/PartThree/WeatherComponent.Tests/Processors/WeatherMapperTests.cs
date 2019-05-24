using System.Collections.Generic;
using System.IO;
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
    public class WeatherMapperTests
    {
        private readonly ILogger _logger;
        private readonly WeatherMapper _weatherMapper;

        public WeatherMapperTests()
        {
            _logger = Substitute.For<ILogger>();
            _weatherMapper = new WeatherMapper(_logger);
        }

        [Fact]
        public async Task Test_get_weather_data_with_good_data_returns_expected_list()
        {
            // Arrange.
            var expectedList = new List<IDataType>
            {
                new ContainingDataType
                {
                    Data =
                        new Weather
                        {
                            Day = 1,
                            MaximumTemperature = 12.6f,
                            MinimumTemperature = 8.1f
                        }
                },
                new ContainingDataType
                {
                    Data =
                        new Weather
                        {
                            Day = 2,
                            MaximumTemperature = 15f,
                            MinimumTemperature = 9.3f
                        }
                }
            };

            // Act.
            var actual = await _weatherMapper.MapAsync(GetGoodData()).ConfigureAwait(true);

            // Assert.
            actual.Should().BeEquivalentTo(expectedList);
        }

        [Fact]
        public async Task Test_get_weather_date_with_invalid_data_logs_invalid_weather()
        {
            // Arrange.
            // Act.
            await _weatherMapper.MapAsync(GetInvalidData()).ConfigureAwait(true);

            // Assert.
            _logger.Received(1).Warning(Arg.Is<string>(l => l.Contains("(MapAsync): Item not valid:")));
        }

        [Fact]
        public async Task Test_get_weather_date_with_invalid_file_throws_exception()
        {
            // Arrange.
            // Act.
            // Assert.
            await Assert.ThrowsAsync<InvalidDataException>(() =>
                _weatherMapper.MapAsync(GetBadData())).ConfigureAwait(true);
        }

        #region Test Data

        private string[] GetGoodData()
        {
            return new[]
            {
                "  Dy MxT   MnT   AvT   HDDay  AvDP 1HrP TPcpn WxType PDir AvSp Dir MxS SkyC MxR MnR AvSLP",
                "  ",
                "   1  12.6   8.1  74          53.8       0.00 F       280  9.6 270  17  1.6  93 23 1004.5",
                "   2  15*    9.3  71          46.5       0.00         330  8.7 340  23  3.3  70 28 1004.5",
                "  mo  82.9  60.5  71.7    16  58.8       0.00              6.9          5.3"
            };
        }

        private string[] GetInvalidData()
        {
            return new[]
            {
                "  Dy MxT   MnT   AvT   HDDay  AvDP 1HrP TPcpn WxType PDir AvSp Dir MxS SkyC MxR MnR AvSLP",
                "  ",
                "   1  10.6  18.1  74          53.8       0.00 F       280  9.6 270  17  1.6  93 23 1004.5",
                "   2  15*    9.3  71          46.5       0.00         330  8.7 340  23  3.3  70 28 1004.5",
                "  mo  82.9  60.5  71.7    16  58.8       0.00              6.9          5.3"
            };
        }

        private string[] GetBadData()
        {
            return new[]
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
