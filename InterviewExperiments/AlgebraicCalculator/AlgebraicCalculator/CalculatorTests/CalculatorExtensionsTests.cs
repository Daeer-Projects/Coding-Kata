using Calculator;
using FluentAssertions;
using Xunit;

namespace CalculatorTests
{
    public class CalculatorExtensionsTests
    {
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
            const string input = "1*4 + 5 5 2-3 + 6/8";

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

            // Act.
            var actual = input.CalculateValue();

            // Assert.
            actual.Should().Be(expected);
        }
    }
}
