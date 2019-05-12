using System.Collections.Generic;
using System.IO.Abstractions;
using System.Threading.Tasks;

using DataMungingCore.Interfaces;
using Easy.MessageHub;
using Serilog;

namespace DataMungingCore
{
    public class ComponentRegister
    {
        public IList<IComponent> Components;
        public IMessageHub Hub;
        private readonly ILogger _logger;

        public ComponentRegister(IMessageHub hub, ILogger logger)
        {
            Hub = hub;
            _logger = logger;
            Components = new List<IComponent>();
        }

        public bool RegisterComponent(IComponentCreator creator, IFileSystem file, ILogger logger)
        {
            // If any of these things are null, then return false.
            Components.Add(creator.CreateComponent(file, logger));
            return true; // For now...
        }

        public async Task<IList<IReturnType>> ProcessComponents()
        {
            _logger.Information($"{GetType().Name} (ProcessComponents): Starting processing the components.");
            var resultList = new List<IReturnType>();

            foreach (var component in Components)
            {
                _logger.Debug($"{GetType().Name} (ProcessComponents): Processing: {component.FileLocation}.");
                var result = await component.Processor.ProcessAsync(component.FileLocation).ConfigureAwait(false);
                resultList.Add(result);
            }

            return resultList;
        }
    }
}
