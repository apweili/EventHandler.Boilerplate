using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using MongoDb.Console.App.DTO;
using MongoDb.Console.MongoDB.Entities;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;

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
        private readonly IRepository<ContractEventLogInfo> _eventDealWithInfoRepository;

        public EventTaskScheduler(IRepository<ContractEventLogInfo> eventDealWithInfoRepository)
        {
            _eventDealWithInfoRepository = eventDealWithInfoRepository;
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

        private async Task<ContractEventLogInfo> GetLatestEventDealWithInfo(ContractEventDetailsETO eventDetailsEto)
        {
            //To Do sync
            return await _eventDealWithInfoRepository.SingleOrDefaultAsync(x => x.Id == eventDetailsEto.GetId());
        }
        
        private async Task SaveEventDealWithInfo(ContractEventLogInfo eventDetailsEto, bool isUpdate)
        {
            // To Do sync
            if (isUpdate)
                await _eventDealWithInfoRepository.UpdateAsync(eventDetailsEto);
            else
                await _eventDealWithInfoRepository.InsertAsync(eventDetailsEto);
        }
    }
}