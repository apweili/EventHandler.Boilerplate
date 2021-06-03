using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDb.Console.App.DTO;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.DependencyInjection;

namespace MongoDb.Console.App
{
    public class ContractEventDetailsHandler: AsyncBackgroundJob<ContractEventDetailsETO>,
        ITransientDependency
    {
        private readonly IEnumerable<IEventProcessor> _eventProcessors;

        public ContractEventDetailsHandler(IEnumerable<IEventProcessor> eventProcessors)
        {
            _eventProcessors = eventProcessors;
        }
        
        public override async Task ExecuteAsync(ContractEventDetailsETO eventData)
        {
            foreach (var processor in _eventProcessors)
            {
                await processor.HandleEventAsync(eventData);
            }
        }
    }
}