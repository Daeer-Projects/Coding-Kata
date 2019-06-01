using System;
using System.Threading.Tasks;

using DataMungingCoreV2.Interfaces;
using DataMungingCoreV2.Processors;
using Easy.MessageHub;
using Serilog;

namespace FootballComponentV2.Processors
{
    /// <summary>
    /// The processing system for the football component.
    /// </summary>
    public class FootballProcessor : IProcessor
    {
        private readonly IReader _footballReader;
        private readonly IMapper _footballMapper;
        private readonly IWriter _footballWriter;
        private readonly IMessageHub _messageHub;
        private readonly ILogger _logger;

        public FootballProcessor(IReader reader, IMapper mapper, IWriter writer, IMessageHub hub, ILogger logger)
        {
            // Contract requirements.
            _footballReader = reader ?? throw new ArgumentNullException(nameof(reader), "The file reader can't be null.");
            _footballMapper = mapper ?? throw new ArgumentNullException(nameof(reader), "The data mapper can't be null.");
            _footballWriter = writer ?? throw new ArgumentNullException(nameof(writer), "The writer can't be null.");
            _messageHub = hub ?? throw new ArgumentNullException(nameof(hub), "The hub can't be null.");
            _logger = logger ?? throw new ArgumentNullException(nameof(logger), "The logger can't be null.");
        }

        /// <summary>
        /// Processes the file location and returns the team with the least points difference.
        /// </summary>
        /// <param name="fileLocation"> The full path to the file we are processing. </param>
        /// <returns> The team with the least points difference. </returns>
        public async Task ProcessAsync(string fileLocation)
        {
            // Contract requirements.
            if (string.IsNullOrWhiteSpace(fileLocation)) throw new ArgumentNullException(nameof(fileLocation), "The file location can not be null.");
            
            _logger.Information($"{GetType().Name} (ProcessAsync): Starting to process the file: {fileLocation}.");
            var result = await Processor.ProcessorWork(fileLocation, _footballReader, _footballMapper, _footballWriter)
                .ConfigureAwait(false);

            _logger.Information($"{GetType().Name} (ProcessAsync): Publishing the result: {result.ProcessResult}.");
            _messageHub.Publish(result);
        }
    }
}
