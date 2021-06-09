using System;
using System.Numerics;
using Volo.Abp.Domain.Entities;

namespace MongoDb.Console.MongoDB.Entities
{
    public class ContractEventLogInfo : IEntity<string>
    {
        public string Id { get; }
        public ContractEventLogInfo(string eventId)
        {
            Id = eventId;
        }
        public string Token { get; set; }
        public BigInteger LastLogBlock { get; set; }
        public string ContractAddress { get; set; }
        public string NodeName { get; set; }
        public BigInteger Timestamp { get; set; }
        public string EventName { get; set; }
        public object[] GetKeys()
        {
            return new object[] {Id};
        }
    }
}