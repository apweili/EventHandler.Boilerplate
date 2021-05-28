using System;
using Volo.Abp.Domain.Entities;

namespace MongoDb.Console.MongoDB.Entities
{
    public class BookType: Entity<Guid>
    {
        public string Type { get; set; }
    }
    
    public class Book: AggregateRoot<Guid>
    {
        public string Name { get; set; }

        public BookType Type { get; set; }
    }
}