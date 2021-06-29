using System.Threading.Tasks;
using AElf.EthereumNode.EventHandler.ETO;
using AElf.EthereumNode.EventHandler.Providers;
using Microsoft.Extensions.Logging;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EventBus.Distributed;

namespace AElf.EthereumNode.EventHandler
{
    public class EthereumEventSerialHandler : IDistributedEventHandler<ContractEventDetailsEto>,
        ITransientDependency
    {
        private readonly IEthereumEventSerialProcessorProvider _ethereumEventSerialProcessors;
        private readonly ILogger<EthereumEventParallelHandler> _logger;
        

        public EthereumEventSerialHandler(IEthereumEventSerialProcessorProvider ethereumEventSerialProcessors, ILogger<EthereumEventParallelHandler> logger)
        {
            _ethereumEventSerialProcessors = ethereumEventSerialProcessors;
            _logger = logger;
        }

        public async Task HandleEventAsync(ContractEventDetailsEto eventData)
        {
            if (_ethereumEventSerialProcessors.GetSerialProcessor(eventData.GetId(), out var processor))
            {
                await processor.HandleEventAsync(eventData);
            }
        }
    }
}