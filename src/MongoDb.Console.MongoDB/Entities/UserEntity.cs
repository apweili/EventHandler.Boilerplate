using System;
using Volo.Abp.Domain.Entities;

namespace MongoDb.Console.MongoDB.Entities
{
    public class UserEntity : IEntity<long>
    {
        public long Id { get; }
        public UserEntity(long id)
        {
            Id = id;
        }
        public string Name { get; set; }
        public int Age { get; set; }
        public int Gender { get; set; }
        public object[] GetKeys()
        {
            return new object[] { Id};
        }

    }
}