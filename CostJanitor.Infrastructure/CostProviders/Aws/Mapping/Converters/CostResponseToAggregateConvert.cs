using Amazon.CostExplorer.Model;
using AutoMapper;
using CostJanitor.Domain.Aggregates;
using ResourceProvisioning.Abstractions.Aggregates;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CostJanitor.Infrastructure.CostProviders.Aws.Mapping.Converters
{
    public class CostResponseToAggregateConvert : ITypeConverter<GetCostAndUsageResponse, Tuple<IAggregateRoot, IEnumerable<IAggregateRoot>>>
    {
        public Tuple<IAggregateRoot, IEnumerable<IAggregateRoot>> Convert(GetCostAndUsageResponse source, Tuple<IAggregateRoot, IEnumerable<IAggregateRoot>> destination, ResolutionContext context)
        {
            switch (source)
            {
                case GetCostAndUsageResponse dto:
                    // This assumes that only GetMonthlyTotalCostAllAccounts and GetMonthlyTotalCostByAccountId has been called. If one were to pass through a GetCostAndUsageResponse with wildly different request parameters, I'd imagine this could very likely go wrong.
                    var reportAggr = new ReportItem(Guid.NewGuid());
                    var costItems = new List<IAggregateRoot>();

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
                        
                        
                        var costItem = new CostItem("monthlyTotalCost", resultByTime.Groups.First().Metrics["BlendedCost"].Amount, assumedCapabilityIdentifier);
                        costItems.Add(costItem);
                        
                        reportAggr.AddCostItem(assumedCapabilityIdentifier);
                    }

                    return new Tuple<IAggregateRoot, IEnumerable<IAggregateRoot>>(reportAggr, costItems);

                default:
                    return null;
            }
        }
    }
}