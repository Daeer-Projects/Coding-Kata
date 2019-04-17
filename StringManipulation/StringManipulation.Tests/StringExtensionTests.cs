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
        
        [Theory]
        [InlineData("AASDEDFFRDSSSSEDSW", 0, "ASDEDFRDSEDSW")]
        [InlineData("AASDEDFFRDSSSSEDSW", -1, "ASDEDFRDSEDSW")]
        [InlineData("AASDEDFFRDSSSSEDSW", -1000, "ASDEDFRDSEDSW")]
        public void Test_string_with_defined_count_of_zero_or_less_returns_expected_as_count_of_one(string input, int count, string expected)
        {
            // Arrange.
            // Act.
            var actual = input.Manipulate(count);

            // Assert.
            actual.Should().Be(expected,
                "using a defined count or zero or less will use one as the count.");
        }
    }
}
