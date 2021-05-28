using MongoDb.Console.MongoDB.Entities;
using MongoDB.Driver;
using Volo.Abp.Data;
using Volo.Abp.MongoDB;

namespace MongoDb.Console.MongoDB
{
    public class MongoDbContext: AbpMongoDbContext
    {
        public IMongoCollection<UserEntity> Users => Collection<UserEntity>();
        
        //[ConnectionStringName("mongodb://127.0.0.1:27017")]
        [ConnectionStringName("Default")]
        protected override void CreateModel(IMongoModelBuilder modelBuilder)
        {
            base.CreateModel(modelBuilder);

            modelBuilder.Entity<UserEntity>(b =>
            {
                /* Sharing the same "AbpUsers" collection
                 * with the Identity module's IdentityUser class. */
                b.CollectionName = "Users";
            });
        }
    }
}