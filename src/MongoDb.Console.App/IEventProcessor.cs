using System.Numerics;
using System.Threading.Tasks;
using MongoDb.Console.App.DTO;

namespace MongoDb.Console.App
{
    public interface IEventProcessor
    {
        string GetEventId();
        Task HandleEventAsync(ContractEventDetailsETO eventDetailsEto);
        BigInteger GetLatestEventTimestamp();
        void SaveLatestEventTimestamp(ContractEventDetailsETO eventDetailsEto);
        bool IsParallelExecute();
        string GetParallelKey(ContractEventDetailsETO eventDetailsEto);
    }
}