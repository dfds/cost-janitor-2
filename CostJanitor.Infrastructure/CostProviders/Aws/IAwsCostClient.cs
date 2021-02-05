using System.Threading.Tasks;
using Amazon.CostExplorer.Model;
using CostJanitor.Infrastructure.CostProviders.Aws.Model;

namespace CostJanitor.Infrastructure.CostProviders.Aws
{
    public interface IAwsCostClient : ICostProvider
    {
        public Task<GetCostAndUsageResponse> GetMonthlyTotalCostAllAccounts();
        public Task<GetCostAndUsageResponse> GetMonthlyTotalCostByAccountId(string accountId);
    }
}