using System.Collections.Generic;
using System.Linq;
using Volo.Abp.DependencyInjection;

namespace AElf.EthereumNode.EventHandler.Providers
{
    public interface IEthereumEventParallelProcessorProvider
    {
        bool GetParallelProcessor(string eventId, out IEthereumEventParallelProcessor processor);
    }
    
    public class EthereumEventParallelProcessorProvider : IEthereumEventParallelProcessorProvider, ISingletonDependency
    {
        private readonly Dictionary<string, IEthereumEventParallelProcessor> _processorDic;
        public EthereumEventParallelProcessorProvider(IEnumerable<IEthereumEventParallelProcessor> parallelProcessors)
        {
            _processorDic = parallelProcessors.ToDictionary(x => x.EventId, x => x);
        }
        
        public bool GetParallelProcessor(string eventId, out IEthereumEventParallelProcessor processor)
        {
            return _processorDic.TryGetValue(eventId, out processor);
        }
    }
}