using AutoMapper;
using CloudEngineering.CodeOps.Abstractions.Events;
using CostJanitor.Domain.Aggregates;
using CostJanitor.Infrastructure.CostProviders.Aws;
using System.Text.Json;
using System.Threading.Tasks;

namespace CostJanitor.Application.Mapping.Converters
{
    public class AwsContextAccountCreatedEventToReportRootConverter : ITypeConverter<IIntegrationEvent, ReportRoot>
    {
        public readonly IMapper _mapper;
        public readonly IAwsCostClient _awsCostClient;
        public readonly IAwsCostClient _costClient;

        public AwsContextAccountCreatedEventToReportRootConverter(IMapper mapper, IAwsCostClient costClient)
        {
            _mapper = mapper;
            _costClient = costClient;
        }

        public ReportRoot Convert(IIntegrationEvent source, ReportRoot destination, ResolutionContext context)
        {
            JsonElement? payload = null;

            if (source.Payload.Value.ValueKind == JsonValueKind.Object)
            {
                var test = source.Payload.Value.GetRawText();
                payload = source.Payload;
            }
            else
            {
                switch (source.Payload.Value.ValueKind)
                {
                    case JsonValueKind.String:
                        var rawText = source.Payload.Value.GetRawText();
                        var cleanedText = rawText.Substring(1, rawText.Length - 2).Replace("\\", "");

                        payload = JsonDocument.Parse(cleanedText).RootElement;

                        break;
                    default:
                        throw new ApplicationFacadeException($"Unsupported ValueKind: {source.Payload.Value.ValueKind}");
                }
            }

            var accountId = payload?.GetProperty("accountId").GetString();
            var getTotalCostTask = _costClient.GetMonthlyTotalCostByAccountIdAsync(accountId);

            Task.WaitAll(getTotalCostTask);

            var totalCost = getTotalCostTask.Result;
            var capabilityId = payload.Value.GetProperty("capabilityId").GetString();

            return _mapper.Map<ReportRoot>(totalCost, opts => opts.Items["CapabilityId"] = capabilityId);
        }
    }
}
