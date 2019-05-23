using FluentAssertions;
using FootballComponent.Extensions;
using Xunit;

namespace FootballComponent.Tests.Extensions
{
    public class StringExtensionTests
    {

        [Theory]
        [InlineData(" hello")]
        [InlineData(" goodbye")]
        [InlineData("1. Arsenal         38    26   9   3    79  -  36    87")]
        [InlineData("dshljfkahsjdhfahfaiuheuifhalsfhshadlkfhlusenhcariheuhrnacucheclakehrclakserxmakjehjancjaskdnhfclau")]
        [InlineData("      Team            W     L    P   D    A      F     Pts")]
        public void Test_to_football_with_invalid_string_sets_is_valid_to_false(string inputString)
        {
            // Arrange.
            // Act.
            var result = inputString.ToFootball();

            // Assert.
            result.IsValid.Should().BeFalse("the data file is not convertible to a football instance.");
        }

        [Theory]
        [InlineData(" hello")]
        [InlineData(" goodbye")]
        [InlineData(" ")]
        public void Test_to_football_with_short_invalid_string_sets_error_list(string inputString)
        {
            // Arrange.
            // Act.
            var result = inputString.ToFootball();

            // Assert.
            result.ErrorList.Should().Contain(x => x.Contains("Exception raised:"));
        }

        [Theory]
        [InlineData(" hello to who ever gets to read this!  I hope it is OK for you! Enjoy! :)")]
        [InlineData(" goodbye to the same person who is reading this.  I hope you have fun! :)")]
        [InlineData("      Teameees            Won     Lost    Played   Drawn    Against      For     Pts")]
        public void Test_to_football_with_long_invalid_string_sets_error_list(string inputString)
        {
            // Arrange.
            // Act.
            var result = inputString.ToFootball();

            // Assert.
            result.ErrorList.Should().Contain(x => x.Contains("Invalid items."));
        }

        [Theory]
        [InlineData("    1. Arsenal         38    26   9   3    -1  -  36    87")]
        [InlineData("    1.                 38    26   9   3    79  -  36    87")]
        [InlineData("    1. Arsenal         38    26   9   3    79  -  -1    87")]
        public void Test_to_football_with_valid_format_but_invalid_items_sets_error_list(string inputString)
        {
            // Arrange.
            // Act.
            var result = inputString.ToFootball();

            // Assert.
            result.ErrorList.Should().Contain(x => x.Contains("Invalid Football."));
        }
    }
}
