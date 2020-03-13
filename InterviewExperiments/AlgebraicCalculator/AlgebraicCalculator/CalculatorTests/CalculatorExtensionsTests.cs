using Calculator;
using FluentAssertions;
using Xunit;

namespace CalculatorTests
{
    public class CalculatorExtensionsTests
    {
        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        public void Test_calculate_with_empty_input_returns_zero(string input)
        {
            // Arrange.
            const string expected = "0";

            // Act.
            var actual = input.CalculateValue();

            // Assert.
            actual.Should().Be(expected);
        }

        [Theory]
        [InlineData("hello")]
        [InlineData("a")]
        [InlineData("!£$%^&")]
        [InlineData(",.|;:'@#~[]{}`¬")]
        [InlineData("<>?=")]
        [InlineData("*-/+()-*/")]
        public void Test_calculate_with_invalid_input_returns_zero(string input)
        {
            // Arrange.
            const string expected = "0";

            // Act.
            var actual = input.CalculateValue();

            // Assert.
            actual.Should().Be(expected);
        }

        [Fact]
        public void Test_calculate_with_simple_input_returns_expected()
        {
            // Arrange.
            const string expected = "2";
            const string input = "1 + 1";

            // Act.
            var actual = input.CalculateValue();

            // Assert.
            actual.Should().Be(expected);
        }

        [Fact]
        public void Test_calculate_with_plus_and_minus_input_returns_expected()
        {
            // Arrange.
            const string expected = "3";
            const string input = "2-1 + 2";

            // Act.
            var actual = input.CalculateValue();

            // Assert.
            actual.Should().Be(expected);
        }

        [Fact]
        public void Test_calculate_with_plus_and_minus_and_multiplication_input_returns_expected()
        {
            // Arrange.
            const string expected = "35/4";
            const string input = "1*4 + 5 + 2-3 + 6/8";

            // Act.
            var actual = input.CalculateValue();

            // Assert.
            actual.Should().Be(expected);
        }

        [Fact]
        public void Test_calculate_with_complicated_input_returns_expected()
        {
            // Arrange.
            const string expected = "233/52";
            const string input = "7-8/6* 6  + 25 / 4-20  / 78*  3  ";
            //                    7 - 8 / 6 * 6 + 25 / 4 - 20 / 78 * 3
            //                    7 - 8 / 6 * 6 + 25 / 4 - 0.25641 * 3
            //                    7 - 8 / 6 * 6 + 6.25 - 0.25641 * 3
            //                    7 - 1.33333 * 6 + 6.25 - 0.25641 * 3
            //                    7 - 1.33333 * 6 + 6.25 - 0.76923
            //                    7 - 7.99998 + 6.25 - 0.76923
            //                    7 - 14.24998 - 0.76923
            //                    -7.24998 - 0.76923
            //                    -8.01921


            //                   7 - 1.33333 * 6 + 25 / 4 - 20 / 78 * 3
            //                   7 - 1.33333 * 6 + 6.25 - 20 / 78 * 3
            //                   7 - 1.33333 * 6 + 6.25 - 0.25641 * 3
            //                   7 - 7.999998 + 6.25 - 0.25641 * 3
            //                   7 - 7.999998 + 6.25 - 0.76923
            //                   0.99998 + 6.25 - 0.76923
            //                   0.99998 + 5.48077
            //                   6.48075



            //                    7-1.33333* 6+ 6.25 - 0.25641 * 3
            //                    7- 7.99998 + 6.25 - 0.76923
            //                    .99998 + 5.48077
            //                    6.48075

            // Act.
            var actual = input.CalculateValue();

            // Assert.
            actual.Should().Be(expected);
        }
    }
}
