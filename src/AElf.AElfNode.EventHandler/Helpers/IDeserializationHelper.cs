using System.Linq;
using AElf.AElfNode.EventHandler.ETO;
using AElf.CSharp.Core;
using AElf.Types;
using Google.Protobuf;
using Volo.Abp.DependencyInjection;
using AElf.AElfNode.EventHandler.Extensions;

namespace AElf.AElfNode.EventHandler.Helpers
{
    public interface IDeserializationHelper
    {
        T DeserializeAElfEvent<T>(LogEventEto data) where T : IEvent<T>, new();
    }
    
    public class AElfEventDeserialization : IDeserializationHelper, ISingletonDependency
    {
        public T DeserializeAElfEvent<T>(LogEventEto eventLog) where T : IEvent<T>, new()
        {
            var logEvent = new LogEvent
            {
                Indexed = {eventLog.Indexed.Select(ByteString.FromBase64)},
                NonIndexed = ByteString.FromBase64(eventLog.NonIndexed)
            };
            T message = default;
            message.MergeFrom(logEvent);
            return message;
        }
    }
}