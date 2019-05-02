using System;
using System.Collections.Generic;

using DataMungingPartTwo.Interfaces;
using DataMungingPartTwo.Processors;
using DataMungingPartTwo.Types;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace DataMungingPartTwo.Tests.Processors
{
    public class TeamProcessorTests
    {
        private readonly IReader _reader;
        private readonly INotify _notify;
        private TeamProcessor _teamProcessor;

        public TeamProcessorTests()
        {
            _reader = Substitute.For<IReader>();
            _notify = Substitute.For<INotify>();
            _teamProcessor = new TeamProcessor(_reader, _notify);
        }

        [Theory]
        [MemberData(nameof(GetMixedConstructorParameters))]
        public void Test_construction_with_mixed_null_parameters_throws_null_exception(IReader reader, INotify notify)
        {
            // Arrange.
            // Act.
            // Assert.
            Assert.Throws<ArgumentNullException>(() => _teamProcessor = new TeamProcessor(reader, notify));
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public void Test_get_team_with_invalid_file_location_throws_exception(string fileLocation)
        {
            // Arrange.
            // Act.
            // Assert.
            Assert.Throws<ArgumentNullException>(() => _teamProcessor.GetTeamWithLeastPointDifference(fileLocation));
        }

        [Fact]
        public void Test_get_team_with_valid_input_and_data_returns_expected_day()
        {
            // Arrange.
            const string expected = "banana";
            const string input = "fullFile";

            _reader.GetFootballData(Arg.Any<string>()).Returns(new List<Football>());
            _notify.GetTeamWithSmallestPointRange(Arg.Any<List<Football>>()).Returns("banana");

            // Act.
            var actual = _teamProcessor.GetTeamWithLeastPointDifference(input);

            // Assert.
            actual.Should().Be(expected);
        }

        #region Test Data.

        public static IEnumerable<object[]> GetMixedConstructorParameters
        {
            get
            {
                yield return new object[]
                {
                    null,
                    Substitute.For<INotify>()
                };
                yield return new object[]
                {
                    Substitute.For<IReader>(),
                    null
                };
                yield return new object[]
                {
                    null,
                    null
                };
            }
        }

        #endregion Test Data.
    }
}
