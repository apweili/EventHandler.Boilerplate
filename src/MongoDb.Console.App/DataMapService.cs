using System.Threading.Tasks;
using MongoDb.Console.MongoDB.Entities;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;

namespace MongoDb.Console.App
{
    public class DataMapService : ITransientDependency
    {
        private IRepository<UserEntity> _userRepository;

        public DataMapService(IRepository<UserEntity> userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task ResolveMessage()
        {
            await AddUser();
        }

        private async Task AddUser()
        {
           await _userRepository.InsertAsync(new UserEntity
            {
                Name = "zqm",
                Age = 1,
                Sex = "female"
            });
        }
    }
}
