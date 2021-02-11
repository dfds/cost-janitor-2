using System;
using System.Collections.Generic;
using System.Linq;
using Amazon.CostExplorer.Model;
using AutoMapper;
using CostJanitor.Infrastructure.CostProviders.Aws.DataTransferObjects;

namespace CostJanitor.Infrastructure.CostProviders.Aws.Mapping.Converters
{
    public class GetCostAndUsageResponseToAwsCostDto : ITypeConverter<GetCostAndUsageResponse, AwsCostDto>
    {
        public AwsCostDto Convert(GetCostAndUsageResponse source, AwsCostDto destination, ResolutionContext context)
        {
            destination ??= new AwsCostDto();

            destination.DimensionValueAttributes = source.DimensionValueAttributes?.Select(o => new AwsDimensionValueAttributeDto
            {
                Attributes = o.Attributes,
                Value = o.Value
            });
            
            destination.ResultsByTime = source.ResultsByTime?.Select(o => new AwsResultByTimeDto
            {
                Total = o.Total?.Select(kvp => new KeyValuePair<string, AwsMetricValueDto>(kvp.Key, new AwsMetricValueDto
                {
                    Amount = kvp.Value.Amount,
                    Unit = kvp.Value.Unit
                })),
                StartDate = DateTime.Parse(o.TimePeriod.Start),
                EndDate = DateTime.Parse(o.TimePeriod.End),
                Estimated = o.Estimated,
                Groups = o.Groups?.Select(g => new AwsGroupDto
                {
                    Keys = g.Keys?.AsEnumerable(),
                    Metrics = g.Metrics?.Select(m => new KeyValuePair<string, AwsMetricValueDto>(m.Key, new AwsMetricValueDto
                    {
                        Amount = m.Value.Amount,
                        Unit = m.Value.Unit
                    }))
                })
            });

            return destination;
        }
    }
}