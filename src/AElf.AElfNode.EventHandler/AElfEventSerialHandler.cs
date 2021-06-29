using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AElf.AElfNode.EventHandler.ETO;
using Microsoft.Extensions.Logging;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EventBus.Distributed;

namespace AElf.AElfNode.EventHandler
{
    public class AElfEventSerialHandler : IDistributedEventHandler<TransactionResultListEto>,
        ITransientDependency
    {
        private readonly IEnumerable<IAElfEventSerialProcessor> _eventProcessors;
        private readonly ILogger<AElfEventSerialHandler> _logger;

        public AElfEventSerialHandler(ILogger<AElfEventSerialHandler> logger,
            IEnumerable<IAElfEventSerialProcessor> eventProcessors)
        {
            _logger = logger;
            _eventProcessors = eventProcessors;
        }

        public async Task HandleEventAsync(TransactionResultListEto eventData)
        {
            var processor = _eventProcessors.FirstOrDefault(x => x.IsMatch(eventData));
            if (processor != null)
                await processor.HandleEventAsync(eventData);
        }
    }
}