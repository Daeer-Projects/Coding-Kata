using FluentAssertions;
using Xunit;

namespace StringManipulation.Tests
{
    public class StringExtensionTests
    {
        [Theory]
        [InlineData("AASDEDFFRDSSSSEDSW", "ASDEDFRDSEDSW")]
        [InlineData("FFDGDGEREGSFGGFGFFGFFFFFSSESSSSSEES", "FDGDGEREGSFGFGFGFSESES")]
        public void Test_string_with_default_count_returns_expected(string input, string expected)
        {
            // Arrange.
            // Act.
            var actual = input.Manipulate();

            // Assert.
            actual.Should().Be(expected,
                "using the default count will return only a single character for each collection in the input string.");
        }

        [Theory]
        [InlineData("AASDEDFFRDSSSSEDSW", 2, "AASDEDFFRDSSEDSW")]
        [InlineData("FFDGDGEREGSFGGFGFFGFFFFFSSESSSSSEES", 3, "FFDGDGEREGSFGGFGFFGFFFSSESSSEES")]
        [InlineData("AAAAAAAAAAAAAAAAAAAA", 7, "AAAAAAA")]
        public void Test_string_with_defined_count_unless_zero_returns_expected(string input, int count, string expected)
        {
            // Arrange.
            // Act.
            var actual = input.Manipulate(count);

            // Assert.
            actual.Should().Be(expected,
                "using the defined count will return the amount of characters for each collection in the input string.");
        }

        [Fact]
        public void Test_string_with_defined_count_of_zero_returns_expected()
        {
            // Arrange.
            var expected = "ASDEDFRDSEDSW";
            var input = "AASDEDFFRDSSSSEDSW";

            // Act.
            var actual = input.Manipulate(0);

            // Assert.
            actual.Should().Be(expected,
                "using the defined count will return the amount of characters for each collection in the input string.");
        }
    }
}
