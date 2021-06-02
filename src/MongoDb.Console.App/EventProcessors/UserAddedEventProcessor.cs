using System.Threading.Tasks;
using MongoDb.Console.App.DTO;
using MongoDb.Console.App.Provider;
using MongoDb.Console.MongoDB.Entities;
using Volo.Abp.Domain.Repositories;

namespace MongoDb.Console.App.EventProcessors
{
    public class UserAddedEventProcessor : IEventProcessor
    {
        public UserAddedEventProcessor(string eventId, IContractDetailsDeserialize deserialize,
            IRepository<UserEntity> userAddedEventRepository, INodeManagerProvider nodeManagerProvider)
        {
            EventId = eventId;
            _deserialize = deserialize;
            _userAddedEventRepository = userAddedEventRepository;
            _nodeManagerProvider = nodeManagerProvider;
        }

        private string EventId { get; }
        private readonly IContractDetailsDeserialize _deserialize;
        private readonly IRepository<UserEntity> _userAddedEventRepository;
        private INodeManagerProvider _nodeManagerProvider;

        public bool IsUserAddedEvent(ContractEventDetails eventDetails)
        {
            return eventDetails.GetId() == EventId;
        }

        public async Task HandleEventAsync(ContractEventDetails eventDetails)
        {
            if (!IsUserAddedEvent(eventDetails))
                return;
            
            await _userAddedEventRepository.InsertAsync(TransferToEntity(eventDetails));
        }

        private UserEntity TransferToEntity(ContractEventDetails eventDetails)
        {
            var userAddedEvent = _deserialize.Deserialize<UserAddedEvent>(eventDetails);
            //use web3 or aelf client to get some information from chain
            var nodeManager = _nodeManagerProvider.GetNodeManager(eventDetails.NodeName);
            //deal with eventDetails
            return null;
        }
    }
}