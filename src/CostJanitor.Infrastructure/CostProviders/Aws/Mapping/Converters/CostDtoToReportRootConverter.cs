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

            var costItems = new List<CostItem>();

            if (source.DimensionValueAttributes != null && source.DimensionValueAttributes.Any())
            {
                foreach (var dimensionValueAttribute in source.DimensionValueAttributes)
                {
                    var awsAccountName = dimensionValueAttribute.Attributes.Single(o => o.Key == "description").Value;
                    var awsAccountId = dimensionValueAttribute.Value;

                    foreach (var resultByTime in source.ResultsByTime.Where(o => o.Groups.Any(g => g.Keys.Any(k => k == awsAccountId))))
                    {
                        var assumedCapabilityIdentifier = awsAccountName.StartsWith("dfds-") ? awsAccountName.Remove(0, 5) : awsAccountName;
                        var blendCostMetricUnitGroups = resultByTime.Groups.Where(g => g.Keys.Any(k => k == awsAccountId)).SelectMany(g => g.Metrics.Where(o => o.Key == "BlendedCost")).GroupBy(m => m.Value.Unit);
                        var totalCost = 0d;

                        foreach (var metricUnitGroup in blendCostMetricUnitGroups) 
                        {
                            //TODO: Figure out if we need to deal with unit bias (e.g. diff currencies being aggregated into the tco). Atm we will assume this isnt the case and apply a magic multiplier of 1
                            var magicMultiplier = 1;

                            foreach (var metric in metricUnitGroup)
                            { 
                                if(double.TryParse(metric.Value.Amount, out var parsedValue))
                                { 
                                    totalCost += parsedValue * magicMultiplier;
                                }
                            }
                        }

                        var costItem = new CostItem("monthlyTotalCost", totalCost.ToString(), assumedCapabilityIdentifier);

                        costItems.Add(costItem);
                    }
                }
            }
            else
            {
                var totalCost = 0d;

                foreach (var metricUnitGroup in source.ResultsByTime.FirstOrDefault().Total.Where(m => m.Key == "BlendedCost").GroupBy(m => m.Value.Unit))
                {
                    //TODO: Figure out if we need to deal with unit bias (e.g. diff currencies being aggregated into the tco). Atm we will assume this isnt the case and apply a magic multiplier of 1
                    var magicMultiplier = 1; 

                    foreach (var metric in metricUnitGroup)
                    {
                        if (double.TryParse(metric.Value.Amount, out var parsedValue))
                        {
                            totalCost += parsedValue * magicMultiplier;
                        }
                    }
                }

                var costItem = new CostItem("monthlyTotalCost", totalCost.ToString(), string.Empty);

                costItems.Add(costItem);
            }

            destination.AddCostItem(costItems);

            return destination;
        }
    }
}