namespace AElf.AElfNode.EventHandler.Options
{
    public class AElfBackgroundJobOption
    {
        public int ParallelWorker { get; set; }
        
        public string QueueName { get; set; }
        
        public string ConnectionName { get; set; }
    }
}