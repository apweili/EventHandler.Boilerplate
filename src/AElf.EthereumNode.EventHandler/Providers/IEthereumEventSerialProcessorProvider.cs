using System.Collections.Generic;
using System.Linq;
using Volo.Abp.DependencyInjection;

namespace AElf.EthereumNode.EventHandler.Providers
{
    public interface IEthereumEventSerialProcessorProvider
    {
        bool GetSerialProcessor(string eventId, out IEthereumEventSerialProcessor processor);
    }

    public class EthereumEventSerialProcessorProvider : IEthereumEventSerialProcessorProvider, ISingletonDependency
    {
        private readonly Dictionary<string, IEthereumEventSerialProcessor> _processorDic;

        public EthereumEventSerialProcessorProvider(IEnumerable<IEthereumEventSerialProcessor> serialProcessors)
        {
            _processorDic = serialProcessors.ToDictionary(x => x.EventId, x => x);
        }

        public bool GetSerialProcessor(string eventId, out IEthereumEventSerialProcessor processor)
        {
            return _processorDic.TryGetValue(eventId, out processor);
        }
    }
}