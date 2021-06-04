using System;
using System.Threading.Tasks;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.DependencyInjection;

namespace MongoDb.Console.App
{
    public interface ITaskQueue<T>
    {
        Task AddQueue(T eventData, Action<T> action);
    }
    
    public abstract class EventParallelHandler<T> : AsyncBackgroundJob<T>, ITransientDependency
    {
        protected ITaskQueue<T> TaskQueue;
        
        public override async Task ExecuteAsync(T eventData)
        {
            await TaskQueue.AddQueue(eventData, ProcessEvent);
        }

        protected void ProcessEvent(T eventData)
        {
            throw new NotImplementedException();
        }
    }
}