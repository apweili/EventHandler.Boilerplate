using System.Numerics;
using System.Threading.Tasks;
using MongoDb.Console.App.DTO;

namespace MongoDb.Console.App
{
    public abstract class EventProcessorBase : IEventProcessor
    {
        private readonly string _eventId;
        private readonly IEventTaskScheduler _eventTaskScheduler;

        protected EventProcessorBase(IEventTaskScheduler eventTaskScheduler)
        {
            _eventTaskScheduler = eventTaskScheduler;
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

        public virtual BigInteger GetLatestEventTimestamp()
        {
            throw new System.NotImplementedException();
        }

        public void SaveLatestEventTimestamp(ContractEventDetailsETO eventDetailsEto)
        {
            throw new System.NotImplementedException();
        }
    }
}