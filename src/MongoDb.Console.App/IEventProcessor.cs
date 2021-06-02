using System.Threading.Tasks;
using MongoDb.Console.App.DTO;

namespace MongoDb.Console.App
{
    public interface IEventProcessor
    {
        public string NodeName { get; }
        public string ContractAddress { get; }
        public string EventName { get; }
        Task HandleEventAsync(ContractEventDetails eventDetails);
    }
}