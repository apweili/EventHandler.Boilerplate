using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using MongoDb.Console.App.DTO;
using Volo.Abp.DependencyInjection;

namespace MongoDb.Console.App
{

    public class ContractProcessProvider
    {
        public ContractEventDetailsETO EventInfo { get; set; }
        public Action<ContractEventDetailsETO> EventProvider { get; set; }
    }
    
    public interface IEventTaskScheduler
    {
        void QueueParallelTask(ContractProcessProvider task);
    }

    public class EventTaskScheduler : IEventTaskScheduler, ISingletonDependency
    {

        private readonly ActionBlock<ContractProcessProvider> _parallelTasks;

        public EventTaskScheduler()
        {
            _parallelTasks = new ActionBlock<ContractProcessProvider>(x => x.EventProvider(x.EventInfo),
                new ExecutionDataflowBlockOptions
                {
                    MaxDegreeOfParallelism = 10
                });
        }
        
        public void QueueParallelTask(ContractProcessProvider task)
        {
            _parallelTasks.Post(task);
        }
    }
}