using AutoMapper;
using CloudEngineering.CodeOps.Infrastructure.AmazonWebServices.DataTransferObjects.Cost;
using CostJanitor.Domain.Aggregates;
using CostJanitor.Domain.ValueObjects;
using System.Collections.Generic;
using System.Linq;

namespace CostJanitor.Infrastructure.CostProviders.Aws.Mapping.Converters
{
    public class CostDtoToReportRootConverter : ITypeConverter<CostDto, ReportRoot>
    {
        public ReportRoot Convert(CostDto source, ReportRoot destination, ResolutionContext context)
        {
            destination ??= new ReportRoot();

            var accountResults = new Dictionary<string, string>();
            
            foreach (var dimensionValueAttribute in source.DimensionValueAttributes)
            {
                var awsAccountName = dimensionValueAttribute.Attributes.Single(o => o.Key == "description").Value;
                var awsAccountId = dimensionValueAttribute.Value;
            
                accountResults.Add(awsAccountId, awsAccountName);
            }

            foreach (var resultByTime in source.ResultsByTime)
            {
                var awsAccountName = accountResults[resultByTime.Groups.First().Keys.First()];

                //TODO: Will ignore v1 capabilities atm
                var assumedCapabilityIdentifier = "";

                if (awsAccountName.Contains("dfds-"))
                {
                    assumedCapabilityIdentifier = awsAccountName.Remove(0, 5);
                }

                var metric = resultByTime.Groups?.First().Metrics?.Single(o => o.Key == "BlendedCost").Value.Amount;
                var costItem = new CostItem("monthlyTotalCost", metric, assumedCapabilityIdentifier);

                destination.AddCostItem(costItem);
            }

            return destination;
        }
    }
}