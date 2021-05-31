using System.Threading.Tasks;
using Nethereum.Web3;

namespace MongoDb.Console.App
{

    public interface INodeManager
    {
        public string NodeName { get; set; }
        public Task<T> CallTransactionResult<T>(params object[] functionInput);
    }
    
    // public class Web3NodeManager: INodeManager
    // {
    //     public async Task<T> CallTransactionResult<T> (params object[] functionInput) 
    //     {
    //         // var web3 = new Web3(account, url);
    //         // var contract = web3.Eth.GetContract(ABI, receipt.ContractAddress);
    //         // var balanceFunction = contract.GetFunction("GetBalance");
    //         // var balance = await balanceFunction.CallDeserializingToObjectAsync<GetBalanceOutputDTO>(account.Address);
    //     }
    // }

    // public class AElfNodeManager : INodeManager
    // {
    //     
    // } 
}