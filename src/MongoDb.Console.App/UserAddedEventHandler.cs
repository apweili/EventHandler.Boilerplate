using System.Threading.Tasks;
using MongoDb.Console.App.DTO;
using MongoDb.Console.App.Provider;
using MongoDb.Console.MongoDB.Entities;
using Volo.Abp.Domain.Repositories;

namespace MongoDb.Console.App
{
    public class UserAddedEventHandler : IEventHandler
    {
        public UserAddedEventHandler(string eventId, IContractDetailsDeserialize deserialize,
            IRepository<UserEntity> registeredEventRepository, INodeManagerProvider nodeManagerProvider)
        {
            EventId = eventId;
            _deserialize = deserialize;
            _registeredEventRepository = registeredEventRepository;
            _nodeManagerProvider = nodeManagerProvider;
        }

        private string EventId { get; }
        private readonly IContractDetailsDeserialize _deserialize;
        private readonly IRepository<UserEntity> _registeredEventRepository;
        private INodeManagerProvider _nodeManagerProvider;

        public bool IsRegisteredEvent(ContractEventDetails eventDetails)
        {
            return eventDetails.GetId() == EventId;
        }

        public async Task HandleEventAsync(ContractEventDetails eventDetails)
        {
            if (!IsRegisteredEvent(eventDetails))
                return;
            
            await _registeredEventRepository.InsertAsync(TransferToEntity(eventDetails));
        }

        private UserEntity TransferToEntity(ContractEventDetails eventDetails)
        {
            var registeredEvent = _deserialize.Deserialize<UserAddedEvent>(eventDetails);
            //use web3 or aelf client to get some information from chain
            //_nodeManagerProvider
            return null;
        }

        private class UserAddedEvent
        {
        }
    }
}