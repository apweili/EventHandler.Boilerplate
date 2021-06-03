using System.Collections.Generic;
using System.Numerics;

namespace MongoDb.Console.App.DTO
{
    public class ContractEventDetailsETO
    {
        public string Name { get; set; }
        public string FilterId { get; set; }
        public string NodeName { get; set; }
        public List<EventParameter> IndexedParameters { get; set; }
        public List<EventParameter> NonIndexedParameters { get; set; }
        public string TransactionHash { get; set; }
        public BigInteger LogIndex { get; set; }
        public BigInteger BlockNumber { get; set; }
        public string BlockHash { get; set; }
        public string Address { get; set; }
        public ContractEventStatus Status { get; set; } = ContractEventStatus.Unconfirmed;
        public string EventSpecificationSignature { get; set; }

        public string NetworkName { get; set; }

        public BigInteger Timestamp { get; set; }
        
        public object[] Topics { get; set; }
        public string Data { get; set; }

        public string GetId()
        {
            return TransactionHash + "-" + BlockHash + "-" + LogIndex;
        }

        public string GetTransactionHash()
        {
            return TransactionHash;
        }

        public string GetBlockHash()
        {
            return BlockHash;
        }
    }
}