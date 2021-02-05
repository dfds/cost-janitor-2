using System.Threading.Tasks;
using Amazon.CostExplorer;
using CostJanitor.Infrastructure.CostProviders.Aws.Model;

namespace CostJanitor.Infrastructure.CostProviders.Aws
{
    public class AwsCostClient : IAwsCostClient
    {
        private IAmazonCostExplorer _amazonCostExplorer;

        public AwsCostClient(IAmazonCostExplorer costExplorerClient)
        {
            _amazonCostExplorer = costExplorerClient;
        }
        
        public async Task<AwsCostResponse> GetMonthlyTotalCostAllAccounts()
        {
            throw new System.NotImplementedException();
        }

        public async Task<AwsCostResponse> GetMonthlyTotalCostByAccountId(string accountId)
        {
            throw new System.NotImplementedException();
        }
    }
}