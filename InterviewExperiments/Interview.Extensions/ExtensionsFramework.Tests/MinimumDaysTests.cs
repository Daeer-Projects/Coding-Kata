using FluentAssertions;
using Xunit;

namespace ExtensionsFramework.Tests
{
    public class MinimumDaysTests
    {
        private readonly MinimumDays _minimumDays;

        public MinimumDaysTests()
        {
            _minimumDays = new MinimumDays();
        }

        [Fact]
        public void Test_minimum_with_test_one_returns_expected()
        {
            // Arrange.
            const int expected = 4;
            const int rows = 5;
            const int columns = 5;
            var grid = new int[,]
            {
                {1, 0, 0, 0, 0},
                {0, 1, 0, 0, 0},
                {0, 0, 1, 0, 0},
                {0, 0, 0, 1, 0},
                {0, 0, 0, 0, 1}
            };

            // Act.
            var result = _minimumDays.minimumDays(rows, columns, grid);

            // Assert.
            result.Should().Be(expected);
        }

        [Fact]
        public void Test_minimum_with_test_two_returns_expected()
        {
            // Arrange.
            const int expected = 3;
            const int rows = 5;
            const int columns = 6;
            var grid = new int[,]
            {
                {0, 0, 1, 0, 0, 0},
                {0, 0, 0, 0, 0, 0},
                {0, 0, 0, 0, 0, 1},
                {0, 0, 0, 0, 0, 0},
                {0, 1, 0, 0, 0, 0}
            };

            // Act.
            var result = _minimumDays.minimumDays(rows, columns, grid);

            // Assert.
            result.Should().Be(expected);
        }
    }
}
