namespace MongoDb.Console.App.Provider
{
    public interface INodeManagerProvider
    {
        INodeManager GetNodeManager(string nodeName);
    }
}