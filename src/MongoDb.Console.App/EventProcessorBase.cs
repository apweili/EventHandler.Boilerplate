using System.Threading.Tasks;
using MongoDb.Console.App.DTO;
using MongoDb.Console.MongoDB.Entities;
using Volo.Abp.Domain.Repositories;

namespace MongoDb.Console.App
{
    public abstract class EventProcessorBase : IEventProcessor
    {
        private readonly string _eventId;
        private readonly IEventTaskScheduler _eventTaskScheduler;
        private readonly IRepository<ContractEventLogInfo> _eventDealWithInfoRepository;

        protected EventProcessorBase(IEventTaskScheduler eventTaskScheduler,
            IRepository<ContractEventLogInfo> eventDealWithInfoRepository)
        {
            _eventTaskScheduler = eventTaskScheduler;
            _eventDealWithInfoRepository = eventDealWithInfoRepository;
            _eventId = "define by user";
        }

        public Task HandleEventAsync(ContractEventDetailsETO eventDetailsEto)
        {
            if (!IsParallelExecute())
            {
                return CustomizeProcess(eventDetailsEto);
            }

            if (string.IsNullOrEmpty(GetParallelKey(eventDetailsEto)))
            {
                _eventTaskScheduler.QueueParallelTask(new ContractProcessProvider
                {
                    EventInfo = eventDetailsEto,
                    EventProvider = async x => await CustomizeProcess(x)
                });
                return Task.CompletedTask;
            }

            _eventTaskScheduler.QueueParallelTask(new ContractProcessProvider
            {
                EventInfo = eventDetailsEto,
                EventProvider = async x => await CustomizeProcess(x)
            });
            return Task.CompletedTask;
        }

        protected abstract Task CustomizeProcess(ContractEventDetailsETO eventDetailsEto);

        public virtual bool IsParallelExecute()
        {
            return false;
        }

        public virtual string GetParallelKey(ContractEventDetailsETO eventDetailsEto)
        {
            return string.Empty;
        }

        public virtual string GetEventId()
        {
            return _eventId;
        }
        
        private async Task<ContractEventLogInfo> GetLatestEventDealWithInfo(ContractEventDetailsETO eventDetailsEto)
        {
            return await _eventDealWithInfoRepository.SingleOrDefaultAsync(x => x.Id == eventDetailsEto.GetId());
        }
        
        private async Task SaveEventDealWithInfo(ContractEventLogInfo eventDetailsEto, bool isUpdate)
        {
            if (isUpdate)
                await _eventDealWithInfoRepository.UpdateAsync(eventDetailsEto);
            else
                await _eventDealWithInfoRepository.InsertAsync(eventDetailsEto);
        }
    }
}