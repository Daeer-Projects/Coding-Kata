using System;

using FluentAssertions;
using Xunit;

namespace RemoveDuplicateCharacters.Tests
{
    public class StringExtensionTests
    {
        [Theory]
        [InlineData(" ", " ")]
        [InlineData("AAABCDDDBCBC", "ABCD")]
        [InlineData("ABCDabcd", "ABCDabcd")]
        public void Test_remove_duplicates_with_default_allowed_duplicates_returns_positive_expected(string input, string expected)
        {
            // Arrange.
            // Act.
            var actual = input.RemoveDuplicateCharacters();

            // Assert.
            actual.Should().Be(expected, "with the given parameters, we should produce the expected.");
        }

        [Theory]
        [InlineData("AAABCDDDBCBC", 0, "ABCD")]
        [InlineData("AAABCDDDBCBC", 1, "AABCDDBC")]
        [InlineData("AAABCDDDBCBC", 2, "AAABCDDDBCBC")]
        [InlineData("AAABCDDDBCBCAAAAAAAAAAAAABBBBBBBC", 7, "AAABCDDDBCBCAAAAABBBBBC")]
        [InlineData(" ", 0, " ")]
        [InlineData("012345012345012345", 1, "012345012345")]
        [InlineData("!!££$$%%^^&&**!!££$$(())", 1, "!!££$$%%^^&&**(())")]
        [InlineData("\t\t", 0, "\t")]
        [InlineData("\r\n\r\n\r\n", 0, "\r\n")]
        public void Test_remove_duplicates_with_valid_parameters_returns_positive_expected(string input, int allowedDuplicates, string expected)
        {
            // Arrange.
            // Act.
            var actual = input.RemoveDuplicateCharacters(allowedDuplicates);

            // Assert.
            actual.Should().Be(expected, "with the given parameters, we should produce the expected.");
        }

        [Theory]
        [InlineData("AAABCDDDBCBC", -1)]
        [InlineData("AAABCDDDBCBC", -1000)]
        public void Test_remove_duplicates_with_invalid_parameters_throws_out_of_range_exception(string input, int allowedDuplicates)
        {
            // Arrange.
            // Act.
            // Assert.
            Assert.Throws<ArgumentOutOfRangeException>(() => input.RemoveDuplicateCharacters(allowedDuplicates));
        }

        [Fact]
        public void Test_remove_duplicates_with_invalid_parameter_throws_null_exception()
        {
            // Arrange.
            // Act.
            // Assert.
            Assert.Throws<ArgumentNullException>(() => string.Empty.RemoveDuplicateCharacters());
        }

        [Fact]
        public void Test_remove_duplicates_with_null_input_throws_null_exception()
        {
            // Arrange.
            string input = null;

            // Act.
            // Assert.
            Assert.Throws<ArgumentNullException>(() => input.RemoveDuplicateCharacters());
        }
    }
}
