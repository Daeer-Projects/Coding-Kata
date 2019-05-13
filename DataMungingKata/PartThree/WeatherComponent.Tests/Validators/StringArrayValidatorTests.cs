using System.Collections.Generic;

using FluentAssertions;
using WeatherComponent.Validators;
using Xunit;

namespace WeatherComponent.Tests.Validators
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
            string[] data = { null, "hello", "banana"};
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
                        "  Dy MxT   MnT   AvT   HDDay  AvDP 1HrP TPcpn WxType PDir AvSp Dir MxS SkyC MxR MnR AvSLP",
                        "  ",
                        "   1  88    59    74          53.8       0.00 F       280  9.6 270  17  1.6  93 23 1004.5",
                        "  mo  82.9  60.5  71.7    16  58.8       0.00              6.9          5.3"
                    }
                };
                yield return new object[]
                {
                    new[]
                    {
                        "  Dy MxT   MnT   AvT   HDDay  AvDP 1HrP TPcpn WxType PDir AvSp Dir MxS SkyC MxR MnR AvSLP",
                        "  ",
                        "   1  88    59    74          53.8       0.00 F       280  9.6 270  17  1.6  93 23 1004.5",
                        "   2  79    63    71          46.5       0.00         330  8.7 340  23  3.3  70 28 1004.5",
                        "  mo  82.9  60.5  71.7    16  58.8       0.00              6.9          5.3"
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
                        "  Dy MxT   MnT   AvT   HDDay  AvDP 1HrP TPcpn WxType PDir AvSp Dir MxS SkyC MxR MnR AvSLP",
                        "  ",
                        "mo  82.9  60.5  71.7    16  58.8       0.00              6.9          5.3"
                    }
                };
            }
        }

        #endregion Test Data.
    }
}
