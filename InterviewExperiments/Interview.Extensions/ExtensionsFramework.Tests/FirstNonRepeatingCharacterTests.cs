using FluentAssertions;
using Xunit;

namespace ExtensionsFramework.Tests
{
    public class FirstNonRepeatingCharacterTests
    {
        [Fact]
        public void Test_get_first_character_with_empty_paragraph_returns_underscore()
        {
            // Arrange.
            const char expected = '_';
            var paragraph = string.Empty;

            // Act.
            var actual = paragraph.GetFirstNonRepeatedCharacter();

            // Assert.
            actual.Should().Be(expected);
        }

        [Theory]
        [InlineData("aaabcccdeeef", 'b')]
        [InlineData("aaabcccdebeef", 'd')]
        public void Test_get_first_character_with_valid_paragraph_returns_expected_character(string paragraph,
            char expected)
        {
            // Arrange.
            // Act.
            var actual = paragraph.GetFirstNonRepeatedCharacter();

            // Assert.
            actual.Should().Be(expected);
        }
    }
}
