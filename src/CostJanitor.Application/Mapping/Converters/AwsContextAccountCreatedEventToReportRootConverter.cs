using AutoMapper;
using CloudEngineering.CodeOps.Abstractions.Events;
using CostJanitor.Domain.Aggregates;
using CostJanitor.Infrastructure.CostProviders.Aws;
using System.Threading.Tasks;

namespace CostJanitor.Application.Mapping.Converters
{
    public class AwsContextAccountCreatedEventToReportRootConverter : ITypeConverter<IIntegrationEvent, ReportRoot>
    {
        public readonly IMapper _mapper;
        public readonly IAwsCostClient _awsCostClient;

        public AwsContextAccountCreatedEventToReportRootConverter(IMapper mapper, IAwsCostClient awsCostClient)
        {
            _mapper = mapper;
            _awsCostClient = awsCostClient;
        }

        public ReportRoot Convert(IIntegrationEvent source, ReportRoot destination, ResolutionContext context)
        {
            var accountId = source.Payload?.GetProperty("accountId").GetString();
            var getTotalCostTask = _awsCostClient.GetMonthlyTotalCostByAccountIdAsync(accountId);

            Task.WaitAll(getTotalCostTask);

            var totalCost = getTotalCostTask.Result;

            return _mapper.Map<ReportRoot>(totalCost);
        }
    }
}
