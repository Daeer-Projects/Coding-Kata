using System.Runtime.InteropServices;
using Calculator;
using FluentAssertions;
using Xunit;

namespace CalculatorTests
{
    /// <summary>
    /// Just a couple of tests to make sure I will get what I think
    /// I should get.
    ///
    /// Just to prove my code is wrong, not the fraction converter.
    /// </summary>
    public class FractionExtensionTests
    {
        [Fact]
        public void Test_to_fraction_with_valid_value_returns_expected()
        {
            // Arrange.
            const string expected = "35/4";
            const double input = 8.75d;

            // Act.
            var actual = input.ToFraction();

            // Assert.
            actual.Should().Be(expected);
        }

        [Fact]
        public void Test_to_fraction_with_large_valid_value_returns_expected()
        {
            // Arrange.
            const string expected = "233/52";
            const double input = 4.4807692307692307692307d;

            // Act.
            var actual = input.ToFraction();

            // Assert.
            actual.Should().Be(expected);
        }


        [Theory]
        [InlineData("25/4", 6.25d)]
        [InlineData("50/3", 16.6666666d)]
        [InlineData("9/2", 4.5d)]
        public void Test_to_fraction_with_valid_values_returns_expected(string expected, double input)
        {
            // Arrange.
            // Act.
            var actual = input.ToFraction();

            // Assert.
            actual.Should().Be(expected);
        }
    }
}
