using System;
using System.Collections.Generic;

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
            // Contract requirements.
            Hub = hub ?? throw new ArgumentNullException(nameof(hub), "The hub can't be null.");
            _logger = logger ?? throw new ArgumentNullException(nameof(logger), "The logger can't be null.");
            Components = new List<IComponent>();
        }

        public bool RegisterComponent(IComponentCreator creator, string fileName)
        {
            var response = false;

            // Contract requirements.
            if (creator != null && !string.IsNullOrWhiteSpace(fileName))
            {
                var component = creator.CreateComponent(Hub, fileName);
                Hub.Subscribe<string>(async (s) => await component.Processor.ProcessAsync(fileName).ConfigureAwait(false));
                Components.Add(component);
                response = true;
            }

            return response;
        }

        public bool RegisterSubscriptions()
        {
            Hub.Subscribe<IReturnType>(r => _logger.Information($"The result is: {r.ProcessResult}."));
            return true;
        }
    }
}
