namespace AElf.EthereumNode.EventHandler.Configuration
{
    public class EthereumBackgroundJobOption
    {
        public int ParallelWorker { get; set; }
        
        public string QueueName { get; set; }
        
        public string ConnectionName { get; set; }
    }
}