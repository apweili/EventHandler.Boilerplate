using System;
using Volo.Abp.Domain.Entities;

namespace MongoDb.Console.MongoDB.Entities
{
    public class UserEntity : Entity<Guid>
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public string Sex { get; set; }
    }
}