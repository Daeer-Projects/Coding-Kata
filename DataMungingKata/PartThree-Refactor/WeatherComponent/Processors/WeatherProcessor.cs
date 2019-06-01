using System;
using System.Threading.Tasks;

using DataMungingCoreV2.Interfaces;
using DataMungingCoreV2.Processors;
using Easy.MessageHub;
using Serilog;

namespace WeatherComponentV2.Processors
{
    public class WeatherProcessor : IProcessor
    {
        private readonly IReader _weatherReader;
        private readonly IMapper _weatherMapper;
        private readonly IWriter _weatherWriter;
        private readonly IMessageHub _messageHub;
        private readonly ILogger _logger;

        public WeatherProcessor(IReader reader, IMapper mapper, IWriter writer, IMessageHub hub, ILogger logger)
        {
            // Contract requirements.
            _weatherReader = reader ?? throw new ArgumentNullException(nameof(reader), "The file reader can't be null.");
            _weatherMapper = mapper ?? throw new ArgumentNullException(nameof(reader), "The data mapper can't be null.");
            _weatherWriter = writer ?? throw new ArgumentNullException(nameof(writer), "The writer can't be null.");
            _messageHub = hub ?? throw new ArgumentNullException(nameof(hub), "The hub can't be null.");
            _logger = logger ?? throw new ArgumentNullException(nameof(logger), "The logger can't be null.");
        }
        
        public async Task ProcessAsync(string fileLocation)
        {
            // Contract requirements.
            if (string.IsNullOrWhiteSpace(fileLocation)) throw new ArgumentNullException(nameof(fileLocation), "The file location can not be null.");
            
            _logger.Information($"{GetType().Name} (ProcessAsync): Starting to process the file: {fileLocation}.");
            var result = await Processor.ProcessorWork(fileLocation, _weatherReader, _weatherMapper, _weatherWriter)
                .ConfigureAwait(false);

            _logger.Information($"{GetType().Name} (ProcessAsync): Publishing the result: {result.ProcessResult}.");
            _messageHub.Publish(result);
        }
    }
}
