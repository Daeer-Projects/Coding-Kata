using System.Collections.Generic;
using FluentAssertions;
using Xunit;

namespace ExtensionsFramework.Tests
{
    public class PopularNToysTests
    {
        private PopularNToys _popularNToys;

        public PopularNToysTests()
        {
            _popularNToys = new PopularNToys();
        }

        [Fact]
        public void Test_popular_with_input_one_returns_expected()
        {
            // Arrange.
            var expected = new List<string>
            {
                "anacell",
                "betacellular"
            };
            const int numToys = 5;
            const int topToys = 2;
            var toys = new List<string>
            {
                "anacell", "betacellular", "cetracular", "deltacellular", "eurocell"
            };
            const int numOfQuotes = 3;
            var quotes = new List<string>
            {
                "Best services provided by anacell", 
                "betacellular has great services",
                "anacell provides much better services than all other"
            };

            // Act.
            var result = _popularNToys.popularNToys(numToys, topToys, toys, numOfQuotes, quotes);

            // Assert.
            result.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public void Test_popular_with_input_two_returns_expected()
        {
            // Arrange.
            var expected = new List<string>
            {
                "betacellular",
                "deltacellular"
            };
            const int numToys = 5;
            const int topToys = 2;
            var toys = new List<string>
            {
                "anacell", "betacellular", "cetracular", "deltacellular", "eurocell"
            };
            const int numOfQuotes = 5;
            var quotes = new List<string>
            {
                "I love anacell Best services provided by anacell in the town",
                "betacellular has great services",
                "deltacellular provides much better services than betacellular",
                "cetracular is worse than eurocell",
                "betacellular is better than deltacellular"
            };

            // Act.
            var result = _popularNToys.popularNToys(numToys, topToys, toys, numOfQuotes, quotes);

            // Assert.
            result.Should().BeEquivalentTo(expected);
        }
    }
}
