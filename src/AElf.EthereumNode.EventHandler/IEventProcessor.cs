using System.Threading.Tasks;
using AElf.EthereumNode.EventHandler.ETO;

namespace AElf.EthereumNode.EventHandler
{
    public interface IEventProcessor
    {
        string EventId { get; }
        bool IsMatch(ContractEventDetailsEto eventDetailsEt);
        Task HandleEventAsync(ContractEventDetailsEto eventDetailsEto);
    }
    
    public interface IEthereumEventSerialProcessor: IEventProcessor
    {
    }
    
    public interface IEthereumEventParallelProcessor: IEventProcessor
    {
    }
}