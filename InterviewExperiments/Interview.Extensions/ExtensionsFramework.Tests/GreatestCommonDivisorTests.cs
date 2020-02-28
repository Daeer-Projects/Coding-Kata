using FluentAssertions;
using Xunit;

namespace ExtensionsFramework.Tests
{
    public class GreatestCommonDivisorTests
    {
        private readonly GreatestCommonDivisor _greatestCommonDivisor;

        public GreatestCommonDivisorTests()
        {
            _greatestCommonDivisor = new GreatestCommonDivisor();
        }

        [Fact]
        public void Test_get_GCD_with_valid_data_returns_expected()
        {
            // Arrange.
            const int expected = 1;
            var numInput = 5;
            var arrayInput = new int[] { 2, 3, 4, 5, 6 };

            // Act.
            var result = _greatestCommonDivisor.generalizedGCD(numInput, arrayInput);

            // Assert.
            result.Should().Be(expected);
        }

        [Fact]
        public void Test_get_GCD_with_second_valid_data_returns_expected()
        {
            // Arrange.
            const int expected = 2;
            var numInput = 5;
            var arrayInput = new int[] { 2, 4, 6, 8, 10 };

            // Act.
            var result = _greatestCommonDivisor.generalizedGCD(numInput, arrayInput);

            // Assert.
            result.Should().Be(expected);
        }
    }
}
