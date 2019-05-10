using System;
using System.Threading.Tasks;

using DataMungingCore.Interfaces;

namespace WeatherComponent.Processors
{
    public class WeatherProcessor : IProcessor
    {
        private readonly IReader _weatherReader;
        private readonly IMapper _weatherMapper;
        private readonly INotify _weatherNotify;

        public WeatherProcessor(IReader reader, IMapper mapper, INotify notify)
        {
            // Contract requirements.
            _weatherReader = reader ?? throw new ArgumentNullException(nameof(reader), "The file reader can't be null.");
            _weatherMapper = mapper ?? throw new ArgumentNullException(nameof(reader), "The data mapper can't be null.");
            _weatherNotify = notify ?? throw new ArgumentNullException(nameof(notify), "The notifier can't be null.");
        }

        //public async Task<IReturnType> ProcessAsync(string fileLocation)
        public IReturnType ProcessAsync(string fileLocation)
        {
            // Contract requirements.
            if (string.IsNullOrWhiteSpace(fileLocation)) throw new ArgumentNullException(nameof(fileLocation), "The file location can not be null.");

            //var weatherData = await _weatherReader.ReadAsync(fileLocation).ConfigureAwait(false);
            //var mappedData = await _weatherMapper.MapAsync(weatherData).ConfigureAwait(false);
            //var result = await _weatherNotify.NotifyAsync(mappedData).ConfigureAwait(false);

            var weatherData = _weatherReader.ReadAsync(fileLocation);
            var mappedData = _weatherMapper.MapAsync(weatherData);
            var result = _weatherNotify.NotifyAsync(mappedData);

            return result;
        }
    }
}
