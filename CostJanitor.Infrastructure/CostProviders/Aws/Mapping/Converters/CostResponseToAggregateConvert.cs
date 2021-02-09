using Amazon.CostExplorer.Model;
using AutoMapper;
using CostJanitor.Domain.Aggregates;
using ResourceProvisioning.Abstractions.Aggregates;
using System;
using System.Collections.Generic;
using System.Linq;

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

                    var accountResults = new Dictionary<string, string>();
                    foreach (var dimensionValueAttribute in dto.DimensionValueAttributes)
                    {
                        var awsAccountName = dimensionValueAttribute.Attributes["description"];
                        var awsAccountId = dimensionValueAttribute.Value;
                        accountResults.Add(awsAccountId, awsAccountName);
                    }

                    foreach (var resultByTime in dto.ResultsByTime)
                    {
                        var awsAccountName = accountResults[resultByTime.Groups.First().Keys.First()];
                        
                        //TODO: Decide whether or not to do magic here
                        var assumedCapabilityIdentifier = "";
                        if (awsAccountName.Contains("dfds-"))
                        {
                            assumedCapabilityIdentifier = awsAccountName.Remove(0, 5);
                        }
                        // Magic end
                        
                        new CostItem("monthlyTotalCost", resultByTime.Groups.First().Metrics["BlendedCost"].Amount, assumedCapabilityIdentifier);
                        
                        reportAggr.AddCostItem(assumedCapabilityIdentifier);
                    }

                    return reportAggr;

                default:
                    return null;
            }
        }
    }
}