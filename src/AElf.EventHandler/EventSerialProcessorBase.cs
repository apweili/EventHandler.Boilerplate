using System.Threading.Tasks;
using AElf.EventHandler.ETO;

namespace AElf.EventHandler
{
    public abstract class EventSerialProcessorBase : IEventSerialProcessor
    {
        protected EventSerialProcessorBase(string eventId)
        {
            EventId = eventId;
        }
        
        public string EventId { get; }

        public Task HandleEventAsync(ContractEventDetailsEto eventDetailsEto)
        {
            return CustomizeProcess(eventDetailsEto);
        }

        protected abstract Task CustomizeProcess(ContractEventDetailsEto eventDetailsEto);
    }
}