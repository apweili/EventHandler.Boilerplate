using System.Threading.Tasks;
using AElf.EthereumNode.EventHandler.ETO;
using AElf.EthereumNode.EventHandler.Providers;
using Microsoft.Extensions.Logging;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.DependencyInjection;

namespace AElf.EthereumNode.EventHandler
{
    public class EthereumEventParallelHandler : IAsyncBackgroundJob<ContractEventDetailsEto>, ISingletonDependency
    {
        private readonly IEthereumEventParallelProcessorProvider _ethereumEventProcessorProvider;
        private readonly ILogger<EthereumEventParallelHandler> _logger;

        public EthereumEventParallelHandler(IEthereumEventParallelProcessorProvider ethereumEventProcessorProvider, ILogger<EthereumEventParallelHandler> logger)
        {
            _ethereumEventProcessorProvider = ethereumEventProcessorProvider;
            _logger = logger;
        }

        public async Task ExecuteAsync(ContractEventDetailsEto eventData)
        {
            if (_ethereumEventProcessorProvider.GetParallelProcessor(eventData.GetId(), out var processor))
            {
                await processor.HandleEventAsync(eventData);
            }
        }
    }
}