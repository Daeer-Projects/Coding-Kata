using FluentAssertions;
using Xunit;

namespace KarateChopKata.Tests
{
    /// <summary>
    /// A collection of tests for the array extension method, "Chop."
    /// </summary>
    public class ArrayExtensionTests
    {
        [Theory]
        [InlineData(-1, new[] { 0 }, 3)]
        [InlineData(-1, new[] { 1 }, 3)]
        [InlineData(-1, new[] { 1, 3, 5 }, 0)]
        [InlineData(-1, new[] { 1, 3, 5 }, 2)]
        [InlineData(-1, new[] { 1, 3, 5 }, 4)]
        [InlineData(-1, new[] { 1, 3, 5 }, 6)]
        [InlineData(-1, new[] { 1, 3, 5, 7 }, 0)]
        [InlineData(-1, new[] { 1, 3, 5, 7 }, 2)]
        [InlineData(-1, new[] { 1, 3, 5, 7 }, 4)]
        [InlineData(-1, new[] { 1, 3, 5, 7 }, 6)]
        [InlineData(-1, new[] { 1, 3, 5, 7 }, 8)]
        public void Test_chop_with_array_returns_expected_negative_results(int expected, int[] inputArray, int searchLocation)
        {
            // Arrange.
            // Act.
            var actual = inputArray.Chop(searchLocation);

            // Assert.
            actual.Should().Be(expected, "the array and location are not compatible and should return -1.");
        }

        [Theory]
        [InlineData(0, new[] { 1 }, 1)]
        [InlineData(0, new[] { 1, 3, 5 }, 1)]
        [InlineData(1, new[] { 1, 3, 5 }, 3)]
        [InlineData(2, new[] { 1, 3, 5 }, 5)]
        [InlineData(0, new[] { 1, 3, 5, 7 }, 1)]
        [InlineData(1, new[] { 1, 3, 5, 7 }, 3)]
        [InlineData(2, new[] { 1, 3, 5, 7 }, 5)]
        [InlineData(3, new[] { 1, 3, 5, 7 }, 7)]
        public void Test_chop_with_array_returns_expected_positive_results(int expected, int[] inputArray, int searchLocation)
        {
            // Arrange.
            // Act.
            var actual = inputArray.Chop(searchLocation);

            // Assert.
            actual.Should().Be(expected, "the values in the search and the array match, so should return the expected.");
        }
    }
}
