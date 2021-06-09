using System.Threading.Tasks;
using MongoDb.Console.App.DTO;
using MongoDb.Console.App.Provider;
using MongoDb.Console.MongoDB.Entities;
using Volo.Abp.Domain.Repositories;

namespace MongoDb.Console.App.EventProcessors
{
    public class UserAddedEventProcessor : EventProcessorBase
    {
        public UserAddedEventProcessor(IContractDetailsDeserialize deserialize,
            IRepository<UserEntity> userAddedEventRepository, INodeManagerProvider nodeManagerProvider,
            IEventTaskScheduler eventTaskScheduler, IRepository<ContractEventLogInfo> eventDealWithInfoRepository) :
            base(eventTaskScheduler, eventDealWithInfoRepository)
        {
            _deserialize = deserialize;
            _userAddedEventRepository = userAddedEventRepository;
            _nodeManagerProvider = nodeManagerProvider;
        }

        private readonly IContractDetailsDeserialize _deserialize;
        private readonly IRepository<UserEntity> _userAddedEventRepository;
        private readonly INodeManagerProvider _nodeManagerProvider;

        public override string GetParallelKey(ContractEventDetailsETO eventDetailsEto)
        {
            throw new System.NotImplementedException();
        }

        protected override async Task CustomizeProcess(ContractEventDetailsETO eventDetailsEto)
        {
            await _userAddedEventRepository.InsertAsync(TransferToEntity(eventDetailsEto));
        }

        public override bool IsParallelExecute()
        {
            return true;
        }

        private UserEntity TransferToEntity(ContractEventDetailsETO eventDetailsEto)
        {
            var userAddedEvent = _deserialize.Deserialize<UserAddedEvent>(eventDetailsEto);
            //use web3 or aelf client to get some information from chain
            var nodeManager = _nodeManagerProvider.GetNodeManager(eventDetailsEto.NodeName);
            //deal with eventDetails
            return null;
        }
    }
}