using System;
using System.Threading.Tasks;

using DataMungingCore.Interfaces;
using Easy.MessageHub;
using Serilog;

namespace FootballComponent.Processors
{
    /// <summary>
    /// The processing system for the football component.
    /// </summary>
    public class FootballProcessor : IProcessor
    {
        private readonly IReader _footballReader;
        private readonly IMapper _footballMapper;
        private readonly INotify _footballNotify;
        private readonly IMessageHub _messageHub;
        private readonly ILogger _logger;

        public FootballProcessor(IReader reader, IMapper mapper, INotify notify, IMessageHub hub, ILogger logger)
        {
            // Contract requirements.
            _footballReader = reader ?? throw new ArgumentNullException(nameof(reader), "The file reader can't be null.");
            _footballMapper = mapper ?? throw new ArgumentNullException(nameof(reader), "The data mapper can't be null.");
            _footballNotify = notify ?? throw new ArgumentNullException(nameof(notify), "The notifier can't be null.");
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

            var footballData = await _footballReader.ReadAsync(fileLocation).ConfigureAwait(false);
            var mappedData = await _footballMapper.MapAsync(footballData).ConfigureAwait(false);
            var result = await _footballNotify.NotifyAsync(mappedData).ConfigureAwait(false);

            _messageHub.Publish(result);
        }
    }
}
