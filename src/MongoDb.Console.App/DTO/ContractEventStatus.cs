namespace MongoDb.Console.App.DTO
{
    public enum ContractEventStatus
    {
        //The transaction that triggered the event has been mined
        Unconfirmed,

        //The configured number of blocks since the event transaction has been reached
        //without a fork in the chain.
        Confirmed,

        //The chain has been forked and the event is no longer valid
        Invalidated
    }
}