using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AElf.AElfNode.EventHandler.ETO;
using Microsoft.Extensions.Logging;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.DependencyInjection;

namespace AElf.AElfNode.EventHandler
{
    public class AElfEventParallelHandler : IAsyncBackgroundJob<TransactionResultListEto>, ISingletonDependency
    {
        private readonly IEnumerable<IAElfEventParallelProcessor> _eventProcessors;
        private readonly ILogger<AElfEventParallelHandler> _logger;

        public AElfEventParallelHandler(ILogger<AElfEventParallelHandler> logger, IEnumerable<IAElfEventParallelProcessor> eventProcessors)
        {
            _logger = logger;
            _eventProcessors = eventProcessors;
        }

        public async Task ExecuteAsync(TransactionResultListEto eventData)
        {
            var processor = _eventProcessors.FirstOrDefault(x => x.IsMatch(eventData));
            if (processor != null)
                await processor.HandleEventAsync(eventData);
                
        }
    }
}