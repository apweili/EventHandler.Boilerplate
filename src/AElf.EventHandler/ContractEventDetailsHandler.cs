using System.Threading.Tasks;
using AElf.EventHandler.ETO;
using AElf.EventHandler.Provider;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EventBus.Distributed;

namespace AElf.EventHandler
{
    public class ContractEventDetailsHandler : IDistributedEventHandler<ContractEventDetailsEto>,
        ITransientDependency
    {
        private readonly IEventParallelProcessorProvider _eventParallelProcessors;
        private readonly IEventSerialProcessorProvider _eventSerialProcessors;

        public ContractEventDetailsHandler(IEventSerialProcessorProvider eventSerialProcessors,
            IEventParallelProcessorProvider eventParallelProcessors)
        {
            _eventSerialProcessors = eventSerialProcessors;
            _eventParallelProcessors = eventParallelProcessors;
        }

        public Task HandleEventAsync(ContractEventDetailsEto eventData)
        {
            var eventId = eventData.GetId();
            if (_eventParallelProcessors.GetParallelProcessor(eventId, out var parallelProcessor))
            {
                
            }

            if (_eventSerialProcessors.GetSerialProcessor(eventId, out var serialProcessor))
            {
                
            }

            return Task.CompletedTask;
        }
    }
}