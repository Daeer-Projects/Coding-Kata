using System.Collections.Generic;
using FluentAssertions;
using FootballComponentV2.Validators;
using Xunit;

namespace FootballComponentV2.Tests.Validators
{
    public class StringArrayValidatorTests
    {
        private readonly StringArrayValidator _arrayValidator;

        public StringArrayValidatorTests()
        {
            _arrayValidator = new StringArrayValidator();
        }

        [Theory]
        [MemberData(nameof(GetGoodData))]
        public void Test_validate_with_valid_array_returns_true(string[] data)
        {
            // Arrange.
            // Act.
            var result = _arrayValidator.Validate(data);

            // Assert.
            result.IsValid.Should().BeTrue("the data provided should produce a true result.");
        }

        [Theory]
        [MemberData(nameof(GetInvalidData))]
        public void Test_validate_with_empty_array_returns_false(string[] data)
        {
            // Arrange.
            // Act.
            var result = _arrayValidator.Validate(data);

            // Assert.
            result.IsValid.Should().BeFalse("the invalid data provided should produce a false result.");
        }

        [Fact]
        public void Test_validate_with_first_item_null_returns_false()
        {
            // Arrange.
            string[] data = { null, "hello", "banana" };
            // Act.
            var result = _arrayValidator.Validate(data);

            // Assert.
            result.IsValid.Should().BeFalse("the null data provided should produce a false result.");
        }

        [Fact]
        public void Test_validate_with_last_item_null_returns_false()
        {
            // Arrange.
            string[] data = { "yes", "hello", null };
            // Act.
            var result = _arrayValidator.Validate(data);

            // Assert.
            result.IsValid.Should().BeFalse("the null data provided should produce a false result.");
        }

        [Fact]
        public void Test_validate_with_middle_item_null_returns_false()
        {
            // Arrange.
            string[] data = { "yes", null, "hello" };
            // Act.
            var result = _arrayValidator.Validate(data);

            // Assert.
            result.IsValid.Should().BeFalse("the null data provided should produce a false result.");
        }

        #region Test Data.

        public static IEnumerable<object[]> GetGoodData
        {
            get
            {
                yield return new object[]
                {
                    new[]
                    {
                        "       Team            P     W    L   D    F      A     Pts",
                        "    1. Arsenal         38    26   9   3    79  -  36    87",
                        "    2. Liverpool       38    24   8   6    67  -  30    80",
                        "    3. Manchester_U    38    24   5   9    87  -  45    77",
                        "   -------------------------------------------------------",
                        "    4. Newcastle       38    21   8   9    74  -  52    71",
                        "    5. Aston Villa     38    21   8   9    29  -  56    71",
                        "    6. Bournemouth     38    21   8   9    61  -  16    71"
                    }
                };
                yield return new object[]
                {
                    new[]
                    {
                        "       Team            P     W    L   D    F      A     Pts",
                        "    1. Arsenal         38    26   9   3    29  -  39    87",
                        "    2. Liverpool       38    24   8   6    57  -  20    80",
                        "    3. Manchester_U    38    24   5   9    97  -  35    77",
                        "   -------------------------------------------------------",
                        "    4. Newcastle       38    21   8   9    71  -  56    71",
                        "    5. Aston Villa     38    21   8   9    79  -  56    71",
                        "    6. Bournemouth     38    21   8   9    91  -  16    71"
                    }
                };
            }
        }

        public static IEnumerable<object[]> GetInvalidData
        {
            get
            {
                yield return new object[]
                {
                    new string[] {}
                };
                yield return new object[]
                {
                    new[] {string.Empty}
                };
                yield return new object[]
                {
                    new[]
                    {
                        "  Oh no, not this one!",
                        "  ",
                        "   47834 2 1.22 424345 yep 12312    43",
                        ":)"
                    }
                };
                yield return new object[]
                {
                    new[]
                    {
                        "       Team            P     W    L   D    F      A     Pts",
                        "    1. Arsenal         38    26   9   3    29  -  39    87",
                        "    2. Liverpool       38    24   8   6    57  -  20    80",
                        "    3. Manchester_U    38    24   5   9    97  -  35    77",
                        "   -------------------------------------------------------",
                        "    4. Newcastle       38    21   8   9    71  -  56    71",
                        "    5. Aston Villa     38    21   8   9    79  -  56    71"
                    }
                };
            }
        }

        #endregion Test Data.
    }
}
