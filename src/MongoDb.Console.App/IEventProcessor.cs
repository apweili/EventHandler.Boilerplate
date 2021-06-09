using System.Numerics;
using System.Threading.Tasks;
using MongoDb.Console.App.DTO;
using MongoDb.Console.MongoDB.Entities;

namespace MongoDb.Console.App
{
    public interface IEventProcessor
    {
        string GetEventId();
        Task HandleEventAsync(ContractEventDetailsETO eventDetailsEto);
        // Task<ContractEventLogInfo> GetLatestEventTimestamp();
        // Task SaveLatestEventTimestamp(ContractEventLogInfo eventDetailsEto);
        bool IsParallelExecute();
        string GetParallelKey(ContractEventDetailsETO eventDetailsEto);
    }
}