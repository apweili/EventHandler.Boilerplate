using System.Threading.Tasks;
using AElf.EventHandler.ETO;

namespace AElf.EventHandler
{
    public interface IEventProcessor
    {
        string EventId { get; }
        Task HandleEventAsync(ContractEventDetailsEto eventDetailsEto);
    }
    
    public interface IEventSerialProcessor: IEventProcessor
    {
    }
    
    public interface IEventParallelProcessor: IEventProcessor
    {
    }
}