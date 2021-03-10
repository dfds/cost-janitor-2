using CloudEngineering.CodeOps.Infrastructure.AmazonWebServices.DataTransferObjects.Cost;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CostJanitor.Infrastructure.CostProviders.Aws
{
    public interface IAwsCostClient : ICostProvider
    {
        Task<IEnumerable<CostDto>> GetMonthlyTotalCostAllAccountsAsync();

        Task<CostDto> GetMonthlyTotalCostByAccountIdAsync(string accountId);
    }
}