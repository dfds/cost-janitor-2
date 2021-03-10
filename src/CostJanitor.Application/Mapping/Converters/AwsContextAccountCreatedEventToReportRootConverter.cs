using AutoMapper;
using CloudEngineering.CodeOps.Abstractions.Events;
using CostJanitor.Domain.Aggregates;
using CostJanitor.Infrastructure.CostProviders.Aws;
using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using System.Text.Json;
using System.Linq;

namespace CostJanitor.Application.Mapping.Converters
{
    public class AwsContextAccountCreatedEventToReportRootConverter : ITypeConverter<IIntegrationEvent, ReportRoot>
    {
        public readonly IMapper _mapper;
        public readonly IAwsCostClient _awsCostClient;
        public readonly IServiceProvider _services;

        public AwsContextAccountCreatedEventToReportRootConverter(IMapper mapper, IServiceProvider services)
        {
            _mapper = mapper;
            _services = services;
        }

        public ReportRoot Convert(IIntegrationEvent source, ReportRoot destination, ResolutionContext context)
        {
            using var serviceScope = _services.CreateScope();

            var awsCostClient = serviceScope.ServiceProvider.GetService<IAwsCostClient>();
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
            var getTotalCostTask = awsCostClient.GetMonthlyTotalCostByAccountIdAsync(accountId);

            Task.WaitAll(getTotalCostTask);

            var totalCost = getTotalCostTask.Result;

            return _mapper.Map<ReportRoot>(totalCost);
        }
    }
}
