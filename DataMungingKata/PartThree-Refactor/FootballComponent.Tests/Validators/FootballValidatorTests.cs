using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using FootballComponentV2.Types;
using FootballComponentV2.Validators;
using Xunit;

namespace FootballComponentV2.Tests.Validators
{
    public class FootballValidatorTests
    {
        private readonly FootballValidator _footballValidator;

        public FootballValidatorTests()
        {
            _footballValidator = new FootballValidator();
        }


        [Theory]
        [MemberData(nameof(GetGoodFootball))]
        public void Test_validate_with_good_data_returns_true(Football football)
        {
            // Arrange.
            // Act.
            var result = _footballValidator.Validate(football);

            // Assert.
            result.IsValid.Should().BeTrue("the football data is all made up of valid data.");
        }

        [Theory]
        [MemberData(nameof(GetBadFootball))]
        public void Test_validate_with_bad_data_returns_false(Football football, int errorCount, List<string> errorMessages)
        {
            // Arrange.
            // Act.
            var result = _footballValidator.Validate(football);

            // Assert.
            // If one fails, then it all should fail.  We will see the 'because' detail.
            result.IsValid.Should().BeFalse("the football data is made up of invalid data.");
            result.Errors.Count.Should().Be(errorCount, "the count should be what I defined.");
            result.Errors.Select(m => m.ErrorMessage).Should().BeEquivalentTo(errorMessages, "the error messages need to match.");
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
                    },
                    1,
                    new List<string>
                    {
                        "Team must have a name."
                    }
                };
                yield return new object[]
                {
                    new Football
                    {
                        TeamName = string.Empty,
                        AgainstPoints = 10,
                        ForPoints = 25
                    },
                    1,
                    new List<string>
                    {
                        "Team must have a name."
                    }
                };
                yield return new object[]
                {
                    new Football
                    {
                        TeamName = "             ",
                        AgainstPoints = 10,
                        ForPoints = 25
                    },
                    1,
                    new List<string>
                    {
                        "Team must have a name."
                    }
                };
                yield return new object[]
                {
                    new Football
                    {
                        TeamName = "Bournemouth",
                        AgainstPoints = -1,
                        ForPoints = 25
                    },
                    1,
                    new List<string>
                    {
                        "Points must be positive.  They can't be less than 0."
                    }
                };
                yield return new object[]
                {
                    new Football
                    {
                        TeamName = "Bournemouth",
                        AgainstPoints = -10,
                        ForPoints = 25
                    },
                    1,
                    new List<string>
                    {
                        "Points must be positive.  They can't be less than 0."
                    }
                };
                yield return new object[]
                {
                    new Football
                    {
                        TeamName = "Bournemouth",
                        AgainstPoints = 25,
                        ForPoints = -1
                    },
                    1,
                    new List<string>
                    {
                        "Points must be positive.  They can't be less than 0."
                    }
                };
                yield return new object[]
                {
                    new Football
                    {
                        TeamName = "Bournemouth",
                        AgainstPoints = 25,
                        ForPoints = -10
                    },
                    1,
                    new List<string>
                    {
                        "Points must be positive.  They can't be less than 0."
                    }
                };
                yield return new object[]
                {
                    new Football
                    {
                        TeamName = "     ",
                        AgainstPoints = -2,
                        ForPoints = -10
                    },
                    3,
                    new List<string>
                    {
                        "Team must have a name.",
                        "Points must be positive.  They can't be less than 0.",
                        "Points must be positive.  They can't be less than 0."
                    }
                };
            }
        }

        #endregion Test Data.
    }
}
