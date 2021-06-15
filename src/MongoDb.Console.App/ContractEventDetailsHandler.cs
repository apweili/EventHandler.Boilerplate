using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDb.Console.App.DTO;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EventBus.Distributed;

namespace MongoDb.Console.App
{
    public class ContractEventDetailsHandler : IDistributedEventHandler<ContractEventDetailsETO>,
        ITransientDependency
    {
        private readonly Dictionary<string, IEventProcessor> _eventProcessors;

        public ContractEventDetailsHandler(IEnumerable<IEventProcessor> eventProcessors)
        {
            _eventProcessors = eventProcessors.ToDictionary(x => x.GetEventId(), x => x);
        }

        public async Task HandleEventAsync(ContractEventDetailsETO eventData)
        {
            if (!_eventProcessors.TryGetValue(eventData.GetId(), out var processor))
            {
                throw new NotImplementedException();
            }

            await processor.HandleEventAsync(eventData);
        }
    }
}