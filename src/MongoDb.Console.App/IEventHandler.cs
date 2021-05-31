using System.Threading.Tasks;
using MongoDb.Console.App.DTO;

namespace MongoDb.Console.App
{
    public interface IEventHandler
    {
        Task HandleEventAsync(ContractEventDetails eventDetails);
    }
}