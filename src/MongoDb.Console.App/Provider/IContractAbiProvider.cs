namespace MongoDb.Console.App.Provider
{
    public interface IContractAbiProvider
    {
        public string GetContractAbi(string node, string contractAddress);
    }
}