using System;
using System.Numerics;
using Volo.Abp.Domain.Entities;

namespace MongoDb.Console.MongoDB.Entities
{
    public class ContractEventLogInfo: Entity<Guid>
    {
        public string EventId { get; set; }
        
        public string Token { get; set; }
        
        public BigInteger LastLogBlock { get; set; }
        
        public string ContractAddress { get; set; }
        
        public string EventName { get; set; }
    }
}