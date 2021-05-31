using MongoDb.Console.App.DTO;

namespace MongoDb.Console.App
{
    public interface IContractDetailsDeserialize
    {
        T Deserialize<T>(ContractEventDetails data);
    }
}