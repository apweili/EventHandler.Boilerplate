using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDb.Console.App.DTO;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EventBus.Distributed;

namespace MongoDb.Console.App
{
    public class ContractEventDetailsHandler: IDistributedEventHandler<ContractEventDetails>,
        ISingletonDependency
    {
        private readonly IEnumerable<IEventProcessor> _eventProcessors;

        public ContractEventDetailsHandler(IEnumerable<IEventProcessor> eventProcessors)
        {
            _eventProcessors = eventProcessors;
        }

        public async Task HandleEventAsync(ContractEventDetails eventData)
        {
            foreach (var processor in _eventProcessors)
            {
                await processor.HandleEventAsync(eventData);
            }
        }
    }
}