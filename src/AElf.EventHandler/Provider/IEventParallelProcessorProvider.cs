using System.Collections.Generic;
using System.Linq;
using Volo.Abp.DependencyInjection;

namespace AElf.EventHandler.Provider
{
    public interface IEventParallelProcessorProvider
    {
        bool GetParallelProcessor(string eventId, out IEventParallelProcessor processor);
    }
    
    public class EventParallelProcessorProvider : IEventParallelProcessorProvider, ISingletonDependency
    {
        private readonly Dictionary<string, IEventParallelProcessor> _processorDic;
        public EventParallelProcessorProvider(IEnumerable<IEventParallelProcessor> parallelProcessors)
        {
            _processorDic = parallelProcessors.ToDictionary(x => x.EventId, x => x);
        }
        
        public bool GetParallelProcessor(string eventId, out IEventParallelProcessor processor)
        {
            return _processorDic.TryGetValue(eventId, out processor);
        }
    }
}