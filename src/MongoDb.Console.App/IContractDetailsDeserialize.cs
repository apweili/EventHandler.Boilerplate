using MongoDb.Console.App.DTO;
using Nethereum.Contracts;
using Nethereum.RPC.Eth.DTOs;
using Nethereum.ABI.FunctionEncoding.Attributes;

namespace MongoDb.Console.App
{
    public interface IContractDetailsDeserialize
    {
        T Deserialize<T>(ContractEventDetails data) where T : IEventDTO, new();
    }

    public class ContractDetailsDeserialize : IContractDetailsDeserialize
    {
        public T Deserialize<T>(ContractEventDetails data) where T : IEventDTO, new()
        {
            var newLog = new FilterLog {Topics = data.Topics};

            return Event<T>.DecodeEvent(newLog).Event;
        }
    }
}