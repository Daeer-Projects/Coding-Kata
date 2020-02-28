using FluentAssertions;
using Xunit;

namespace ExtensionsFramework.Tests
{
    public class CellCompleteTests
    {
        private readonly CellComplete _cellComplete;

        public CellCompleteTests()
        {
            _cellComplete = new CellComplete();
        }

        [Fact]
        public void Test_cell_complete_with_valid_parameters_returns_expected_results()
        {
            // Arrange.
            var expected = new int[] {0, 1, 0, 0, 1, 0, 1, 0};
            var input = new int[] {1, 0, 0, 0, 0, 1, 0, 0};

            // Act.
            var result = _cellComplete.cellComplete(input, 1);

            // Assert.
            for (var element = 0; element < expected.Length - 1; element++)
            {
                result[element].Should().Be(expected[element]);
            }
        }
        
        [Fact]
        public void Test_cell_complete_with_valid_parameters_for_two_days_returns_expected_results()
        {
            // Arrange.
            var expected = new int[] { 0, 0, 0, 0, 0, 1, 1, 0 };

            var input = new int[] { 1, 1, 1, 0, 1, 1, 1, 1 };

            // Act.
            var result = _cellComplete.cellComplete(input, 2);

            // Assert.
            for (var element = 0; element < expected.Length - 1; element++)
            {
                result[element].Should().Be(expected[element]);
            }
        }
    }
}
