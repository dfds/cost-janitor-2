using System.Threading.Tasks;
using CostJanitor.Infrastructure.CostProviders.Aws.Model;

namespace CostJanitor.Infrastructure.CostProviders.Aws
{
    public class AwsCostClient : IAwsCostClient
    {
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