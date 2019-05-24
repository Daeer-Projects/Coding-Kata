using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

using DataMungingCore.Interfaces;
using DataMungingCore.Types;
using FluentAssertions;
using FootballComponent.Processors;
using FootballComponent.Types;
using NSubstitute;
using Serilog;
using Xunit;

namespace FootballComponent.Tests.Processors
{
    public class FootballMapperTests
    {
        private readonly ILogger _logger;
        private readonly FootballMapper _footballMapper;

        public FootballMapperTests()
        {
            _logger = Substitute.For<ILogger>();
            _footballMapper = new FootballMapper(_logger);
        }


        [Fact]
        public async Task Test_get_football_data_with_good_data_returns_expected_list()
        {
            // Arrange.
            var expectedList = new List<IDataType>
            {
                new ContainingDataType
                {
                    Data =
                        new Football
                        {
                            TeamName = "Arsenal",
                            AgainstPoints = 36,
                            ForPoints = 79
                        }
                },
                new ContainingDataType
                {
                    Data =
                        new Football
                        {
                            TeamName = "Liverpool",
                            AgainstPoints = 30,
                            ForPoints = 67
                        }
                },
                new ContainingDataType
                {
                    Data =
                        new Football
                        {
                            TeamName = "Manchester_U",
                            AgainstPoints = 45,
                            ForPoints = 87
                        }
                },
                new ContainingDataType
                {
                    Data =
                        new Football
                        {
                            TeamName = "Bournemouth",
                            AgainstPoints = 16,
                            ForPoints = 61
                        }
                }
            };

            // Act.
            var actual = await _footballMapper.MapAsync(GetGoodData()).ConfigureAwait(true);

            // Assert.
            actual.Should().BeEquivalentTo(expectedList);
        }

        [Fact]
        public async Task Test_get_football_date_with_invalid_data_logs_invalid_weather()
        {
            // Arrange.
            // Act.
            await _footballMapper.MapAsync(GetInvalidData()).ConfigureAwait(true);

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
                _footballMapper.MapAsync(GetBadData())).ConfigureAwait(true);
        }

        #region Test Data

        private string[] GetGoodData()
        {
            return new[]
            {
                "       Team            P     W    L   D    F      A     Pts",
                "    1. Arsenal         38    26   9   3    79  -  36    87",
                "   -------------------------------------------------------",
                "    2. Liverpool       38    24   8   6    67  -  30    80",
                "    3. Manchester_U    38    24   5   9    87  -  45    77",
                "    4. Bournemouth     38    21   8   9    61  -  16    71"
            };
        }

        private string[] GetInvalidData()
        {
            return new[]
            {
                "       Team            P     W    L   D    F      A     Pts",
                "    1. Arsenal         38    26   9   3    79  -  36    87",
                "   -------------------------------------------------------",
                "    2. Liverpool       38    24   8   6    -7  -  30    80",
                "    3. Manchester_U    38    24   5   9    87  -  45    77",
                "    4. Bournemouth     38    21   8   9    61  -  16    71"
            };
        }

        private string[] GetBadData()
        {
            return new[]
            {
                "  Oh no, not this one!",
                "  ",
                " Ar$ena1  38    26   9   3    79  -  36    87 banana",
                ":)"
            };
        }

        #endregion Test Data
    }
}
