using System.Linq;
using AElf.CSharp.Core;
using AElf.EventHandler.ETO;
using AElf.EventHandler.Extensions;
using AElf.Types;
using Google.Protobuf;
using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.Contracts;
using Nethereum.RPC.Eth.DTOs;
using Volo.Abp.DependencyInjection;

namespace AElf.EventHandler.Helper
{
    public interface IDeserializeHelper
    {
        T DeserializeEthereumEvent<T>(ContractEventDetailsEto data) where T : IEventDTO, new();
        
        // T DeserializeAElfEvent<T>(LogEventEto data) where T : IEvent<T>, new();
    }
    
    public class ContractDetailsDeserialize : IDeserializeHelper, ITransientDependency
    {
        public T DeserializeEthereumEvent<T>(ContractEventDetailsEto data) where T : IEventDTO, new()
        {
            var newLog = new FilterLog {Topics = data.Topics, Data = data.Data};

            return Event<T>.DecodeEvent(newLog).Event;
        }

        // public T DeserializeAElfEvent<T>(LogEventEto eventLog) where T : IEvent<T>, new()
        // {
        //     var logEvent = new LogEvent
        //     {
        //         Indexed = {eventLog.Indexed.Select(ByteString.FromBase64)},
        //         NonIndexed = ByteString.FromBase64(eventLog.NonIndexed)
        //     };
        //     T message = default;
        //     message.MergeFrom(logEvent);
        //     return message;
        // }
    }
}