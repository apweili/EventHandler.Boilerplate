using System;
using System.Runtime.Serialization;

namespace MongoDb.Console.App.DTO
{
    public class EventParameter
    {
        public string Type { get; set; }
        public string Value { get; set; }
    }
}