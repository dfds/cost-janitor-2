using Amazon.CostExplorer;
using Amazon.CostExplorer.Model;
using AutoMapper;
using CostJanitor.Infrastructure.CostProviders.Aws.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CostJanitor.Infrastructure.CostProviders.Aws
{
    public class AwsCostClient : IAwsCostClient
    {
        private readonly IAmazonCostExplorer _amazonCostExplorer;
        private readonly IMapper _mapper;

        public AwsCostClient(IAmazonCostExplorer costExplorerClient, IMapper mapper)
        {
            _amazonCostExplorer = costExplorerClient;
            _mapper = mapper;
        }
        
        public async Task<IEnumerable<AwsCostDto>> GetMonthlyTotalCostAllAccountsAsync()
        {
            var result = new List<AwsCostDto>();
            GetCostAndUsageResponse resp = null;

            do
            {
                resp = await _amazonCostExplorer.GetCostAndUsageAsync(new GetCostAndUsageRequest()
                {
                    Metrics = new List<string>(new[] { Metric.BLENDED_COST.Value }),
                    TimePeriod = CreateDateIntervalForCurrentMonth(),
                    Granularity = Granularity.MONTHLY,
                    GroupBy = new List<GroupDefinition>(new[]
                    {
                        new GroupDefinition()
                        {
                            Type = GroupDefinitionType.DIMENSION,
                            Key = Dimension.LINKED_ACCOUNT.Value
                        }
                    }),
                    NextPageToken = resp?.NextPageToken
                });

                result.Add(_mapper.Map<AwsCostDto>(resp));
            }
            while (resp.NextPageToken != null);

            return result;
        }

        public async Task<AwsCostDto> GetMonthlyTotalCostByAccountIdAsync(string accountId)
        {
            var resp = await _amazonCostExplorer.GetCostAndUsageAsync(new GetCostAndUsageRequest()
            {
                Metrics = new List<string>(new []{Metric.BLENDED_COST.Value}),
                TimePeriod = CreateDateIntervalForCurrentMonth(),
                Granularity = Granularity.MONTHLY,
                Filter = new Expression
                {
                    Dimensions = new DimensionValues()
                    {
                        Key = Dimension.LINKED_ACCOUNT,
                        Values = new List<string>(){accountId}
                    }
                }
            });

            return _mapper.Map<AwsCostDto>(resp);
        }

        private DateInterval CreateDateIntervalForCurrentMonth()
        {
            var currentDate = DateTime.Now;
            var firstDayOfMonth = new DateTime(currentDate.Year, currentDate.Month, 1);
            var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);
            
            return new DateInterval
            {
                Start = $"{firstDayOfMonth.Year}-{firstDayOfMonth.Month.ToString("#00")}-{firstDayOfMonth.Day.ToString("#00")}",
                End = $"{lastDayOfMonth.Year}-{lastDayOfMonth.Month.ToString("#00")}-{lastDayOfMonth.Day.ToString("#00")}"
            };
        }
    }
}