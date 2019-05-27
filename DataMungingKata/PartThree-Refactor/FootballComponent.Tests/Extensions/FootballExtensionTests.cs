using System.Collections.Generic;

using DataMungingCoreV2.Extensions;
using FluentAssertions;
using FootballComponentV2.Types;
using FootballComponentV2.Validators;
using Xunit;

namespace FootballComponentV2.Tests.Extensions
{
    public class FootballExtensionTests
    {
        [Theory]
        [MemberData(nameof(GetGoodFootball))]
        public void Test_is_valid_with_valid_football_returns_true(Football football)
        {
            // Arrange.
            // Act.
            var result = football.IsValid<Football, FootballValidator>();

            // Assert.
            result.IsValid.Should().BeTrue("the football data is all made up of valid data.");
        }

        [Theory]
        [MemberData(nameof(GetBadFootball))]
        public void Test_is_valid_with_invalid_football_returns_false(Football football)
        {
            // Arrange.
            // Act.
            var result = football.IsValid<Football, FootballValidator>();

            // Assert.
            result.IsValid.Should().BeFalse("the football data is made up of invalid data.");
        }

        #region Test Data.
        
        public static IEnumerable<object[]> GetGoodFootball
        {
            get
            {
                yield return new object[]
                {
                    new Football
                    {
                        TeamName = "Arsenal",
                        AgainstPoints = 29,
                        ForPoints = 10
                    }
                };
                yield return new object[]
                {
                    new Football
                    {
                        TeamName = "Bournemouth",
                        AgainstPoints = 1,
                        ForPoints = 0
                    }
                };
                yield return new object[]
                {
                    new Football
                    {
                        TeamName = "Bournemouth",
                        AgainstPoints = 1,
                        ForPoints = 100
                    }
                };
            }
        }

        public static IEnumerable<object[]> GetBadFootball
        {
            get
            {
                yield return new object[]
                {
                    new Football
                    {
                        TeamName = null,
                        AgainstPoints = 10,
                        ForPoints = 25
                    }
                };
                yield return new object[]
                {
                    new Football
                    {
                        TeamName = string.Empty,
                        AgainstPoints = 10,
                        ForPoints = 25
                    }
                };
                yield return new object[]
                {
                    new Football
                    {
                        TeamName = "             ",
                        AgainstPoints = 10,
                        ForPoints = 25
                    }
                };
                yield return new object[]
                {
                    new Football
                    {
                        TeamName = "Bournemouth",
                        AgainstPoints = -1,
                        ForPoints = 25
                    }
                };
                yield return new object[]
                {
                    new Football
                    {
                        TeamName = "Bournemouth",
                        AgainstPoints = -10,
                        ForPoints = 25
                    }
                };
                yield return new object[]
                {
                    new Football
                    {
                        TeamName = "Bournemouth",
                        AgainstPoints = 25,
                        ForPoints = -1
                    }
                };
                yield return new object[]
                {
                    new Football
                    {
                        TeamName = "Bournemouth",
                        AgainstPoints = 25,
                        ForPoints = -10
                    }
                };
            }
        }

        #endregion Test Data.
    }
}
