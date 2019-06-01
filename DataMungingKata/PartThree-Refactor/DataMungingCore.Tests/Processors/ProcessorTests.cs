using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DataMungingCoreV2.Interfaces;
using DataMungingCoreV2.Processors;
using DataMungingCoreV2.Types;
using Easy.MessageHub;
using FluentAssertions;
using NSubstitute;
using Serilog;
using Xunit;

namespace DataMungingCoreV2.Tests.Processors
{
    public class ProcessorTests
    {
        private readonly IReader _reader;
        private readonly IMapper _mapper;
        private readonly IWriter _writer;

        public ProcessorTests()
        {
            _reader = Substitute.For<IReader>();
            _mapper = Substitute.For<IMapper>();
            _writer = Substitute.For<IWriter>();
        }

        [Theory]
        [MemberData(nameof(GetMixedConstructorParameters))]
        public async Task Test_process_work_with_mixed_null_parameters_throws_null_exception(IReader reader, IMapper mapper, IWriter writer)
        {
            // Arrange.
            const string input = "fileLocation";

            // Act.
            // Assert.
            await Assert
                .ThrowsAsync<ArgumentNullException>(() => Processor.ProcessorWork(input, reader, mapper, writer))
                .ConfigureAwait(true);
        }
        
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public async Task Test_process_work_with_invalid_file_location_throws_exception(string fileLocation)
        {
            // Arrange.
            // Act.
            // Assert.
            await Assert
                .ThrowsAsync<ArgumentNullException>(() =>
                    Processor.ProcessorWork(fileLocation, _reader, _mapper, _writer)).ConfigureAwait(true);
        }

        [Fact]
        public async Task Test_process_work_with_valid_input_and_data_returns_expected_team()
        {
            // Arrange.
            const string expected = "processResult";
            const string input = "fullFileName";

            _reader.ReadAsync(Arg.Any<string>()).Returns(new[] { "hello" });
            _mapper.MapAsync(Arg.Any<string[]>()).Returns(new List<IDataType>());
            _writer.WriteAsync(Arg.Any<IList<IDataType>>()).Returns(new ContainingResultType { ProcessResult = "processResult" });

            // Act.
            var actual = await Processor.ProcessorWork(input, _reader, _mapper, _writer).ConfigureAwait(true);

            // Assert.
            actual.ProcessResult.Should().Be(expected, "that is what has been set up to be returned.");
        }

        #region Test Data.

        public static IEnumerable<object[]> GetMixedConstructorParameters
        {
            get
            {
                yield return new object[]
                {
                    null,
                    Substitute.For<IMapper>(),
                    Substitute.For<IWriter>()
                };
                yield return new object[]
                {
                    Substitute.For<IReader>(),
                    null,
                    Substitute.For<IWriter>()
                };
                yield return new object[]
                {
                    Substitute.For<IReader>(),
                    Substitute.For<IMapper>(),
                    null
                };
                yield return new object[]
                {
                    null,
                    null,
                    null
                };
            }
        }

        #endregion Test Data.
    }
}
