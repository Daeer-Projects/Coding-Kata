using System;

using DataMungingPartTwo.Interfaces;

namespace DataMungingPartTwo.Processors
{
    public class TeamProcessor
    {
        private readonly IReader _fileReader;

        private readonly INotify _footballNotifier;

        public TeamProcessor(IReader reader, INotify notify)
        {
            // Contract requirements.
            _fileReader = reader ?? throw new ArgumentNullException(nameof(reader), "The file reader can't be null.");
            _footballNotifier = notify ?? throw new ArgumentNullException(nameof(notify), "The football notifier can't be null.");
        }

        public string GetTeamWithLeastPointDifference(string fileLocation)
        {
            // Contract requirements.
            if (string.IsNullOrWhiteSpace(fileLocation)) throw new ArgumentNullException(nameof(fileLocation), "The file location can not be null.");

            var fileData = _fileReader.GetFootballData(fileLocation);
            var result = _footballNotifier.GetTeamWithSmallestPointRange(fileData);

            return result;
        }
    }
}
