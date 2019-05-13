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

        public bool RegisterComponent(IComponentCreator creator)
        {
            // If any of these things are null, then return false.
            var component = creator.CreateComponent(Hub);
            component.Processor.RegisterSubscriptions();
            Components.Add(component);
            return true; // For now...
        }

        //// We don't want to do this, we want an event to start the process for the component.
        //public async Task ProcessComponents()
        //{
        //    _logger.Information($"{GetType().Name} (ProcessComponents): Starting processing the components.");
        //    //var resultList = new List<IReturnType>();

        //    foreach (var component in Components)
        //    {
        //        _logger.Debug($"{GetType().Name} (ProcessComponents): Processing: {component.FileLocation}.");
        //        await component.Processor.ProcessAsync(component.FileLocation).ConfigureAwait(false);
        //    }
        //}
    }
}
