using Nethereum.ABI.FunctionEncoding.Attributes;

namespace MongoDb.Console.App.DTO
{
    [Event("UserAdded")]
    public class UserAddedEvent: IEventDTO
    { 
        [Parameter("uint256", "Id", 1, true)]
        public long Id {get; set;}

        [Parameter("string", "Name", 2, true)]
        public string Name {get; set;}
        
        [Parameter("uint256", "Age", 3, true)]
        public int Age {get; set;}

        [Parameter("int", "Gender", 4, true)]
        public int Gender {get; set;}
    }
}