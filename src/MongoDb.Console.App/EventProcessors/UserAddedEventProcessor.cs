using System.Threading.Tasks;
using MongoDb.Console.App.DTO;
using MongoDb.Console.App.Provider;
using MongoDb.Console.MongoDB.Entities;
using Volo.Abp.Domain.Repositories;

namespace MongoDb.Console.App.EventProcessors
{
    public class UserAddedEventProcessor : IEventProcessor
    {
        public UserAddedEventProcessor(IContractDetailsDeserialize deserialize,
            IRepository<UserEntity> userAddedEventRepository, INodeManagerProvider nodeManagerProvider)
        {
            _deserialize = deserialize;
            _userAddedEventRepository = userAddedEventRepository;
            _nodeManagerProvider = nodeManagerProvider;
        }

        // public string NodeName { get; } = "ethereum";
        // public string ContractAddress { get; } = "0xabcd";
        // public string EventName { get; } = "UserAdded";
        private readonly IContractDetailsDeserialize _deserialize;
        private readonly IRepository<UserEntity> _userAddedEventRepository;
        private readonly INodeManagerProvider _nodeManagerProvider;

        // public bool IsUserAddedEvent(ContractEventDetails eventDetails)
        // {
        //     return EventName == eventDetails.Name && eventDetails.NodeName == NodeName &&
        //            eventDetails.Address == ContractAddress;
        // }

        public async Task HandleEventAsync(ContractEventDetailsETO eventDetailsEto)
        {
            await _userAddedEventRepository.InsertAsync(TransferToEntity(eventDetailsEto));
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