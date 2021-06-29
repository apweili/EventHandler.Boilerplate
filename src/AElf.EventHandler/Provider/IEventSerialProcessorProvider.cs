using System.Collections.Generic;
using System.Linq;
using Volo.Abp.DependencyInjection;

namespace AElf.EventHandler.Provider
{
    public interface IEventSerialProcessorProvider
    {
        bool GetSerialProcessor(string eventId, out IEventSerialProcessor processor);
    }

    public class EventSerialProcessorProvider : IEventSerialProcessorProvider, ISingletonDependency
    {
        private readonly Dictionary<string, IEventSerialProcessor> _processorDic;

        public EventSerialProcessorProvider(IEnumerable<IEventSerialProcessor> serialProcessors)
        {
            _processorDic = serialProcessors.ToDictionary(x => x.EventId, x => x);
        }

        public bool GetSerialProcessor(string eventId, out IEventSerialProcessor processor)
        {
            return _processorDic.TryGetValue(eventId, out processor);
        }
    }
}