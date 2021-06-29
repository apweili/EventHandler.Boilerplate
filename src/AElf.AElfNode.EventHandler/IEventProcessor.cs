using System.Threading.Tasks;
using AElf.AElfNode.EventHandler.ETO;

namespace AElf.AElfNode.EventHandler
{
    public interface IEventProcessor
    {
        bool IsMatch(TransactionResultListEto eventDetailsEt);
        Task HandleEventAsync(TransactionResultListEto eventDetailsEto);
    }
    
    public interface IAElfEventSerialProcessor: IEventProcessor
    {
    }
    
    public interface IAElfEventParallelProcessor: IEventProcessor
    {
    }
}