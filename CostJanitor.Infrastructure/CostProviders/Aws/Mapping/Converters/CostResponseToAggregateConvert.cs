using Amazon.CostExplorer.Model;
using AutoMapper;
using CostJanitor.Domain.Aggregates;
using ResourceProvisioning.Abstractions.Aggregates;
using System;

namespace CostJanitor.Infrastructure.CostProviders.Aws.Mapping.Converters
{
    public class JsonElementToWebHookEventConverter : ITypeConverter<GetCostAndUsageResponse, IAggregateRoot>
    {
        public IAggregateRoot Convert(GetCostAndUsageResponse source, IAggregateRoot destination, ResolutionContext context)
        {
            switch (source)
            {
                case GetCostAndUsageResponse dto:
                    var reportAggr = new ReportItem(Guid.NewGuid());

                    

                    return reportAggr;

                default:
                    return null;
            }
        }
    }
}