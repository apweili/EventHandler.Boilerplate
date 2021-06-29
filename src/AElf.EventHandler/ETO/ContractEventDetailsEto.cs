using System.Collections.Generic;
using System.Numerics;

namespace AElf.EventHandler.ETO
{
    public class ContractEventDetailsEto
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
        public ContractEventStatusEnum StatusEnum { get; set; } = ContractEventStatusEnum.Unconfirmed;
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
    
    public enum ContractEventStatusEnum
    {
        //The transaction that triggered the event has been mined
        Unconfirmed,

        //The configured number of blocks since the event transaction has been reached
        //without a fork in the chain.
        Confirmed,

        //The chain has been forked and the event is no longer valid
        Invalidated
    }
    
    public class EventParameter
    {
        public string Type { get; set; }
        public string Value { get; set; }
    }
}