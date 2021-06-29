using AElf.EthereumNode.EventHandler.ETO;
using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.Contracts;
using Nethereum.RPC.Eth.DTOs;
using Volo.Abp.DependencyInjection;

namespace AElf.EthereumNode.EventHandler.Helper
{
    public interface IDeserializationHelper
    {
        T DeserializeEthereumEvent<T>(ContractEventDetailsEto data) where T : IEventDTO, new();
    }
    
    public class ContractDetailsDeserialization : IDeserializationHelper, ISingletonDependency
    {
        public T DeserializeEthereumEvent<T>(ContractEventDetailsEto data) where T : IEventDTO, new()
        {
            var newLog = new FilterLog {Topics = data.Topics, Data = data.Data};

            return Event<T>.DecodeEvent(newLog).Event;
        }
    }
}