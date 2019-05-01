using System;
using System.Collections.Generic;

using DataMungingPartTwo.Processors;
using DataMungingPartTwo.Types;
using FluentAssertions;
using Xunit;

namespace DataMungingPartTwo.Tests.Processors
{
    public class FootballNotifierTests
    {
        private readonly FootballNotifier _footballNotifier;

        public FootballNotifierTests()
        {
            _footballNotifier = new FootballNotifier();
        }

        [Fact]
        public void Test_get_team_with_empty_list_throws_exception()
        {
            // Arrange.
            var data = new List<Football>();

            // Act.
            // Assert.
            Assert.Throws<ArgumentException>(() => _footballNotifier.GetTeamWithSmallestPointRange(data));
        }

        [Fact]
        public void Test_get_team_with_null_list_throws_exception()
        {
            // Arrange.
            // Act.
            // Assert.
            Assert.Throws<ArgumentNullException>(() => _footballNotifier.GetTeamWithSmallestPointRange(null));
        }

        [Fact]
        public void Test_get_team_with_invalid_points_throws_exception()
        {
            // Arrange.
            var footballData = new List<Football>
            {
                    new Football {TeamName = "Arsenal", ForPoints = -1, AgainstPoints = -10000 }
            };

            // Act.
            // Assert.
            Assert.Throws<DataMisalignedException>(() => _footballNotifier.GetTeamWithSmallestPointRange(footballData));
        }

        [Theory]
        [MemberData(nameof(GetValidData))]
        public void Test_get_team_with_valid_list_returns_expected(string expectedDay, IList<Football> data)
        {
            // Arrange.
            // Act.
            var actualDay = _footballNotifier.GetTeamWithSmallestPointRange(data);

            // Assert.
            actualDay.Should().Be(expectedDay);
        }

        #region Test Data.

        public static IEnumerable<object[]> GetValidData
        {
            get
            {
                yield return new object[]
                {
                    "Arsenal",
                    new List<Football>
                    {
                        new Football {TeamName = "Arsenal", ForPoints = 50, AgainstPoints = 49},
                        new Football {TeamName = "Tottenham", ForPoints = 45, AgainstPoints = 65},
                        new Football {TeamName = "Liverpool", ForPoints = 58, AgainstPoints = 38},
                    }
                };
                yield return new object[]
                {
                    "Tottenham",
                    new List<Football>
                    {
                        new Football {TeamName = "Arsenal", ForPoints = 10, AgainstPoints = 100},
                        new Football {TeamName = "Tottenham", ForPoints = 5, AgainstPoints = 5},
                        new Football {TeamName = "Liverpool", ForPoints = 1, AgainstPoints = 0},
                    }
                };
            }
        }

        #endregion Test Data.
    }
}
