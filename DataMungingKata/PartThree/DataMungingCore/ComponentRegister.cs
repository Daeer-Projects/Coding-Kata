using System.Collections.Generic;
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

        public bool RegisterComponent(IComponentCreator creator, string fileName)
        {
            // If any of these things are null, then return false.
            var component = creator.CreateComponent(Hub, fileName);
            Hub.Subscribe<string>(async (s) => await component.Processor.ProcessAsync(fileName).ConfigureAwait(false));
            Components.Add(component);
            return true; // For now...
        }

        public bool RegisterSubscriptions()
        {
            Hub.Subscribe<IReturnType>(r => _logger.Information($"The result is: {r.ProcessResult}."));
            return true; // For now...
        }
    }
}
