using System;
using System.Threading.Tasks;

using DataMungingCore.Interfaces;
using Easy.MessageHub;
using Serilog;

namespace WeatherComponent.Processors
{
    public class WeatherProcessor : IProcessor
    {
        private readonly IReader _weatherReader;
        private readonly IMapper _weatherMapper;
        private readonly INotify _weatherNotify;
        private readonly IMessageHub _messageHub;
        private readonly ILogger _logger;

        public WeatherProcessor(IReader reader, IMapper mapper, INotify notify, IMessageHub hub, ILogger logger)
        {
            // Contract requirements.
            _weatherReader = reader ?? throw new ArgumentNullException(nameof(reader), "The file reader can't be null.");
            _weatherMapper = mapper ?? throw new ArgumentNullException(nameof(reader), "The data mapper can't be null.");
            _weatherNotify = notify ?? throw new ArgumentNullException(nameof(notify), "The notifier can't be null.");
            _messageHub = hub ?? throw new ArgumentNullException(nameof(hub), "The hub can't be null.");
            _logger = logger ?? throw new ArgumentNullException(nameof(logger), "The logger can't be null.");
        }
        
        public async Task ProcessAsync(string fileLocation)
        {
            // Contract requirements.
            if (string.IsNullOrWhiteSpace(fileLocation)) throw new ArgumentNullException(nameof(fileLocation), "The file location can not be null.");

            var weatherData = await _weatherReader.ReadAsync(fileLocation).ConfigureAwait(false);
            var mappedData = await _weatherMapper.MapAsync(weatherData).ConfigureAwait(false);
            var result = await _weatherNotify.NotifyAsync(mappedData).ConfigureAwait(false);

            _messageHub.Publish(result);
        }
    }
}
