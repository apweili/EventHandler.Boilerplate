using System.Numerics;

namespace MongoDb.Console.MongoDB.Entities
{
    public abstract class EventInfoBase
    {
        public string Name { get; set; }
        public string FilterId { get; set; }
        public string NodeName { get; set; }
        public string TransactionHash { get; set; }
        public BigInteger LogIndex { get; set; }
        public BigInteger BlockNumber { get; set; }
        public string BlockHash { get; set; }
        public string Address { get; set; }
    }
}