using System;
using System.Collections.Generic;
using System.IO.Abstractions;
using System.Threading.Tasks;

using DataMungingCoreV2.Processors;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace DataMungingCoreV2.Tests.Processors
{
    public class ReaderTests
    {
        private readonly IFile _file;

        public ReaderTests()
        {
            _file = Substitute.For<IFile>();
        }

        [Fact]
        public async Task Test_read_work_with_valid_parameters_returns_expected()
        {
            // Arrange.
            const string input = "fileLocation";
            string[] expected = { "hello", "goodbye", "Pooh", "Tigger" };
            _file.ReadAllLines(Arg.Any<string>()).Returns(expected);

            // Act.
            var actual = await Reader.ReadWork(_file, input).ConfigureAwait(true);

            // Assert.
            actual.Should().BeEquivalentTo(expected);
        }

        [Theory]
        [MemberData(nameof(GetInvalidData))]
        public async Task Test_read_work_with_null_parameters_throws_exception(IFile file, string input)
        {
            // Arrange.
            // Act.
            // Assert.
            await Assert.ThrowsAsync<ArgumentNullException>(() => Reader.ReadWork(file, input)).ConfigureAwait(true);
        }

        [Theory]
        [MemberData(nameof(GetInvalidFileLocation))]
        public async Task Test_read_work_with_invalid_file_location_system_throws_exception(string input)
        {
            // Arrange.
            // Act.
            // Assert.
            await Assert.ThrowsAsync<ArgumentException>(() => Reader.ReadWork(_file, input)).ConfigureAwait(true);
        }

        #region Test Data

        public static IEnumerable<object[]> GetInvalidData
        {
            get
            {
                yield return new object[]
                {
                    null,
                    "fileLocation"
                };
                yield return new object[]
                {
                    Substitute.For<IFile>(),
                    null
                };
            }
        }

        public static IEnumerable<object[]> GetInvalidFileLocation
        {
            get
            {
                yield return new object[]
                {
                    string.Empty
                };
                yield return new object[]
                {
                    " "
                };
                yield return new object[]
                {
                    "   "
                };
            }
        }

        #endregion Test Data
    }
}
