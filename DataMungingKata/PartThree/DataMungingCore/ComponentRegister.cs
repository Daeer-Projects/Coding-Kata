using System;
using System.Collections.Generic;

using DataMungingCore.Interfaces;
using Easy.MessageHub;
using Serilog;

namespace DataMungingCore
{
    /// <summary>
    /// The component registration system.
    /// </summary>
    public class ComponentRegister
    {
        /// <summary>
        /// The components that have been registered in the system.
        /// </summary>
        public IList<IComponent> Components;

        /// <summary>
        /// The hub that deals with all of the events.
        /// </summary>
        private IMessageHub Hub;

        /// <summary>
        /// The logging system.
        /// </summary>
        private readonly ILogger _logger;

        /// <summary>
        /// Initialises a new instance of the ComponentRegister class.
        /// </summary>
        /// <param name="hub"> The event hub. </param>
        /// <param name="logger"> The logging system. </param>
        public ComponentRegister(IMessageHub hub, ILogger logger)
        {
            // Contract requirements.
            Hub = hub ?? throw new ArgumentNullException(nameof(hub), "The hub can't be null.");
            _logger = logger ?? throw new ArgumentNullException(nameof(logger), "The logger can't be null.");
            Components = new List<IComponent>();
        }

        /// <summary>
        /// Uses the creator to create a component, and then add that component to the list.
        /// </summary>
        /// <param name="creator"> The specific component creator. </param>
        /// <param name="fileName"> The filename (location) that the component uses to read from. </param>
        /// <returns></returns>
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

        /// <summary>
        /// Registers the basic response subscription.
        /// </summary>
        /// <returns></returns>
        public bool RegisterSubscriptions()
        {
            Hub.Subscribe<IReturnType>(r => _logger.Information($"The result is: {r.ProcessResult}."));
            return true;
        }
    }
}
