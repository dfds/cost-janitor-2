using System.Threading.Tasks;
using CostJanitor.Infrastructure.CostProviders.Aws.Model;

namespace CostJanitor.Infrastructure.CostProviders.Aws
{
    public interface IAwsCostClient : ICostProvider
    {
        public Task<AwsCostResponse> GetMonthlyTotalCostAllAccounts();
        public Task<AwsCostResponse> GetMonthlyTotalCostByAccountId(string accountId);
    }
}