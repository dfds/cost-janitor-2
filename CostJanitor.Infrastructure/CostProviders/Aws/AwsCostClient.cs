using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Amazon.CostExplorer;
using Amazon.CostExplorer.Model;
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
        
        public async Task<GetCostAndUsageResponse> GetMonthlyTotalCostAllAccounts()
        {
            var currentDate = DateTime.Now;
            var firstDayOfMonth = new DateTime(currentDate.Year, currentDate.Month, 1);
            var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);
            
            var dateInterval = new DateInterval()
            {
                Start = $"{firstDayOfMonth.Year}-{firstDayOfMonth.Month.ToString("#00")}-{firstDayOfMonth.Day.ToString("#00")}",
                End = $"{lastDayOfMonth.Year}-{lastDayOfMonth.Month.ToString("#00")}-{lastDayOfMonth.Day.ToString("#00")}"
            };
            
            
            var resp = await _amazonCostExplorer.GetCostAndUsageAsync(new GetCostAndUsageRequest()
            {
                Metrics = new List<string>(new []{Metric.BLENDED_COST.Value}),
                TimePeriod = dateInterval,
                Granularity = Granularity.MONTHLY,
                GroupBy = new List<GroupDefinition>(new []
                {
                    new GroupDefinition()
                    {
                        Type = GroupDefinitionType.DIMENSION,
                        Key = Dimension.LINKED_ACCOUNT.Value
                    }
                })
            });
            return resp;
        }

        public async Task<GetCostAndUsageResponse> GetMonthlyTotalCostByAccountId(string accountId)
        {
            throw new System.NotImplementedException();
        }
    }
}