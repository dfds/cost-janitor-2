using CostJanitor.Infrastructure.CostProviders.Aws.DataTransferObjects;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CostJanitor.Infrastructure.CostProviders.Aws
{
    public interface IAwsCostClient : ICostProvider
    {
        Task<IEnumerable<AwsCostDto>> GetMonthlyTotalCostAllAccountsAsync();

        Task<AwsCostDto> GetMonthlyTotalCostByAccountIdAsync(string accountId);
    }
}