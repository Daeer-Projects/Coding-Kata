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
        public void Test_calculate_with_math_controls_returns_something_other_than_zero()
        {
            // Arrange.
            const string input = "2-1/4*(3+8)-2*6/2";
            const string expected = "0";

            // Act.
            var actual = input.CalculateValue();

            // Assert.
            actual.Should().NotBe(expected);
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

        [Theory]
        [InlineData("2", "1 + 1")]
        [InlineData("4", "1 + 1+2")]
        [InlineData("8", "1 + 1+2+4")]
        [InlineData("6", "1 + 1+3 + 1")]
        public void Test_calculate_with_simple_additions_returns_expected(string expected, string input)
        {
            // Arrange.
            // Act.
            var actual = input.CalculateValue();

            // Assert.
            actual.Should().Be(expected);
        }

        [Theory]
        [InlineData("0", "1 - 1")]
        [InlineData("7", "10 - 1-2")]
        [InlineData("3", "10 - 1-2-4")]
        [InlineData("5", "10 - 1-3 - 1")]
        public void Test_calculate_with_simple_subtractions_returns_expected(string expected, string input)
        {
            // Arrange.
            // Act.
            var actual = input.CalculateValue();

            // Assert.
            actual.Should().Be(expected);
        }

        [Theory]
        [InlineData("5", "10 / 2")]
        [InlineData("4", "16 / 2 / 2")]
        [InlineData("2", "16 / 1/2/4")]
        [InlineData("4", "24 / 2/3 / 1")]
        [InlineData("9/2", "36 / 2/2 / 2")]
        public void Test_calculate_with_simple_divisions_returns_expected(string expected, string input)
        {
            // Arrange.
            // Act.
            var actual = input.CalculateValue();

            // Assert.
            actual.Should().Be(expected);
        }

        [Theory]
        [InlineData("10", "5 * 2")]
        [InlineData("40", "5*2* 4")]
        [InlineData("144", "12 * 2 *6")]
        public void Test_calculate_with_simple_multiplications_returns_expected(string expected, string input)
        {
            // Arrange.
            // Act.
            var actual = input.CalculateValue();

            // Assert.
            actual.Should().Be(expected);
        }

        [Theory]
        [InlineData("10", "5 + 10/2")]
        [InlineData("9", "5 + 8/2")]
        public void Test_calculate_with_division_after_addition_returns_expected(string expected, string input)
        {
            // Arrange.
            // Act.
            var actual = input.CalculateValue();

            // Assert.
            actual.Should().Be(expected);
        }

        [Theory]
        [InlineData("-15", "5 - 10*2")]
        [InlineData("29", "45 - 8*2")]
        public void Test_calculate_with_multiplication_after_subtraction_returns_expected(string expected, string input)
        {
            // Arrange.
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
            //  BODMAS - D        7 - 1.33333 * 6 + 6.25 - 0.25641 * 3
            //         - M        7 - 8 + 6.25 - 0.76923
            //         - A        7 - 14.25 - 0.76923
            //         - S        -8.01923

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

            // Needs to end up: 4.4807692307692307692307

            // Act.
            var actual = input.CalculateValue();

            // Assert.
            actual.Should().Be(expected);
        }
    }
}
