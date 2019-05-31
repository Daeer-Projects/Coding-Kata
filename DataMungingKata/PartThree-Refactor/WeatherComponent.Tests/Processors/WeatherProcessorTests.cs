using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DataMungingCoreV2.Interfaces;
using DataMungingCoreV2.Types;
using Easy.MessageHub;
using NSubstitute;
using Serilog;
using WeatherComponentV2.Processors;
using Xunit;

namespace WeatherComponentV2.Tests.Processors
{
    public class WeatherProcessorTests
    {
        private readonly IReader _reader;
        private readonly IMapper _mapper;
        private readonly IWriter _writer;
        private readonly ILogger _logger;
        private readonly IMessageHub _messageHub;
        private WeatherProcessor _processor;

        public WeatherProcessorTests()
        {
            _reader = Substitute.For<IReader>();
            _mapper = Substitute.For<IMapper>();
            _writer = Substitute.For<IWriter>();
            _logger = Substitute.For<ILogger>();
            _messageHub = Substitute.For<IMessageHub>();
            _processor = new WeatherProcessor(_reader, _mapper, _writer, _messageHub, _logger);
        }

        [Theory]
        [MemberData(nameof(GetMixedConstructorParameters))]
        public void Test_construction_with_mixed_null_parameters_throws_null_exception(IReader reader, IMapper mapper, IWriter writer, IMessageHub hub, ILogger logger)
        {
            // Arrange.
            // Act.
            // Assert.
            Assert.Throws<ArgumentNullException>(() => _processor = new WeatherProcessor(reader, mapper, writer, hub, logger));
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public async Task Test_process_with_invalid_file_location_throws_exception(string fileLocation)
        {
            // Arrange.
            // Act.
            // Assert.
            await Assert.ThrowsAsync<ArgumentNullException>(() => _processor.ProcessAsync(fileLocation)).ConfigureAwait(true);
        }

        [Fact]
        public async Task Test_process_with_valid_input_and_data_returns_expected_day()
        {
            // Arrange.
            const int expected = 4;
            const string input = "fullFileName";

            _reader.ReadAsync(Arg.Any<string>()).Returns(new[] {"hello"});
            _mapper.MapAsync(Arg.Any<string[]>()).Returns(new List<IDataType>());
            _writer.WriteAsync(Arg.Any<IList<IDataType>>()).Returns(new ContainingResultType {ProcessResult = 4});

            // Act.
            await _processor.ProcessAsync(input).ConfigureAwait(false);

            // Assert.
            _messageHub.Received(1)
                .Publish<IReturnType>(Arg.Is<ContainingResultType>(result => result.ProcessResult.Equals(expected)));
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
                    Substitute.For<IWriter>(),
                    Substitute.For<IMessageHub>(),
                    Substitute.For<ILogger>()
                };
                yield return new object[]
                {
                    Substitute.For<IReader>(),
                    null,
                    Substitute.For<IWriter>(),
                    Substitute.For<IMessageHub>(),
                    Substitute.For<ILogger>()
                };
                yield return new object[]
                {
                    Substitute.For<IReader>(),
                    Substitute.For<IMapper>(),
                    null,
                    Substitute.For<IMessageHub>(),
                    Substitute.For<ILogger>()
                };
                yield return new object[]
                {
                    Substitute.For<IReader>(),
                    Substitute.For<IMapper>(),
                    Substitute.For<IWriter>(),
                    null,
                    Substitute.For<ILogger>()
                };
                yield return new object[]
                {
                    Substitute.For<IReader>(),
                    Substitute.For<IMapper>(),
                    Substitute.For<IWriter>(),
                    Substitute.For<IMessageHub>(),
                    null
                };
                yield return new object[]
                {
                    null,
                    null,
                    null,
                    null,
                    null
                };
            }
        }

        #endregion Test Data.
    }
}
