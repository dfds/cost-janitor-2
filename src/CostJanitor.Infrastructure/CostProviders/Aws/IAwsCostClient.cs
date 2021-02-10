using Amazon.CostExplorer.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CostJanitor.Infrastructure.CostProviders.Aws
{
    public interface IAwsCostClient : ICostProvider
    {
        //TODO: Issue #145 - Implement DTO objects to avoid leaky abstractions
        Task<IEnumerable<GetCostAndUsageResponse>> GetMonthlyTotalCostAllAccounts();

        Task<GetCostAndUsageResponse> GetMonthlyTotalCostByAccountId(string accountId);
    }
}