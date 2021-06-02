using System.Threading.Tasks;
using MongoDb.Console.App.DTO;

namespace MongoDb.Console.App
{
    public interface IEventProcessor
    {
        Task HandleEventAsync(ContractEventDetails eventDetails);
    }
}